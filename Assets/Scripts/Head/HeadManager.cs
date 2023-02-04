using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum ActionType
{
    Lick,
    Tadpole,
    FrogEgg,
    Water
}

public class HeadManager : MonoBehaviour
{
    public GameObject objContent;
    [Header("Asset")]
    public Texture2D texture_Head;
    public SpriteRenderer sr_Head;
    public PhysicsMaterial2D pm_Head;
    public GameObject pfTadpole;
    public Transform tfContentTadpole;
    public PolygonCollider2D triggerHead;

    [Header("SubManager")]
    public FrogAssManager assManager;
    public FrogMouthManager mouthManager;

    [Header("Extra")]

    //ActionType
    private ActionType actionType;
    private HeadUIManager headUIManager;
    //Generate Dynamically
    private Texture2D newTexture;
    private PolygonCollider2D colHead;
    //Some mapping information
    float worldWidth, worldHeight;
    float pixelWidth, pixelHeight;
    //Cool down timer for generating the tadpole
    float timerAction = 0;
    

    public void Init()
    {
        objContent.SetActive(false);
        headUIManager = GameManager.Instance.uiManager.headUIManager;
        assManager.Init(this);
        mouthManager.Init(this);
        timerAction = 2f;
    }

    //StartGame
    public void StartGame()
    {
        objContent.SetActive(true);
        InitHead();
        PublicTool.ClearChildItem(tfContentTadpole);
        ChangeAction(ActionType.Lick);
    }

    #region InitHead
    public void InitHead()
    {
        //According to the asset to create a new texture
        newTexture = new Texture2D(texture_Head.width, texture_Head.height);
        Color[] colors = texture_Head.GetPixels();
        newTexture.SetPixels(colors);
        //Apply and make sprite
        newTexture.Apply();
        MakeSprite();
        //Read and load the mapping information
        worldWidth = sr_Head.bounds.size.x;
        worldHeight = sr_Head.bounds.size.y;
        pixelWidth = sr_Head.sprite.texture.width;
        pixelHeight = sr_Head.sprite.texture.height;

        Debug.Log("World:" + worldWidth + "," + worldHeight + "Pixel:" + pixelWidth + "," + pixelHeight);

        //Generate the collider
        if (colHead != null)
        {
            Destroy(colHead);
        }
        colHead = sr_Head.gameObject.AddComponent<PolygonCollider2D>();
        colHead.sharedMaterial = pm_Head;
    }
    #endregion

    #region AboutAction

    public void Update()
    {
        if (GameManager.Instance.isStartGame)
        {
            timerAction -= Time.deltaTime;

            if (timerAction < 0)
            {
                CheckHoverAction();

                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    CheckClickAction();
                }
            }
        }
    }

    public void ChangeAction(ActionType type)
    {
        //Hide All
        assManager.HideContent();
        mouthManager.HideContent();
        //Set Type
        actionType = type;
        switch (actionType)
        {
            case ActionType.Lick:
                mouthManager.ShowContent();
                break;
            case ActionType.Tadpole:
                assManager.ShowContent();
                break;
        }
    }

    public void CheckClickAction()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePosition = PublicTool.GetMousePosition2D();
            switch (actionType)
            {
                case ActionType.Lick:
                    //LickAction(mousePosition);
                    mouthManager.StartExtendTongue();
                    GameManager.Instance.soundManager.PlaySound(SoundType.Lick);
                    timerAction = GameGlobal.intervalLick;
                    break;
                case ActionType.Tadpole:
                    //BornTadpole(mousePosition);
                    BornTadpole(assManager.GetAssPosition());
                    StartCoroutine(assManager.IE_bornAni());
                    int ranSound = Random.Range(0, 2);
                    if (ranSound == 0)
                    {
                        GameManager.Instance.soundManager.PlaySound(SoundType.BornTadpole0);
                    }
                    else if(ranSound == 1)
                    {
                        GameManager.Instance.soundManager.PlaySound(SoundType.BornTadpole1);
                    }
                    timerAction = GameGlobal.intervalBorn;
                    break;
            }
        }
    }

    public void CheckHoverAction()
    {
        Vector2 mousePosition = PublicTool.GetMousePosition2D();
        switch (actionType)
        {
            case ActionType.Lick:
                mouthManager.SetPosition(mousePosition);
                break;
            case ActionType.Tadpole:
                assManager.SetPosition(mousePosition);
                break;
        }
    }

    #endregion

    #region Dot

    //Dig a hole
    public void CheckClickMakeHole(Vector2 pos)
    {
        Collider2D overCollider2D = Physics2D.OverlapCircle(pos, 0.01f, LayerMask.GetMask("Head"));
        if (overCollider2D != null)
        {
            MakeHole(pos);
        }
    }

    //When click the pixel, make a dot
    public void MakeHole(Vector2 pos)
    {
        Vector2Int pixelPos = WorldToPixel(pos);
        Debug.Log(pixelPos);
        int radius = GameGlobal.HoleRadius;

        int px, nx, py, ny, distance;

        for(int i = 0;i< radius;i++)
        {
            distance = Mathf.RoundToInt(Mathf.Sqrt(radius * radius - i * i));
            for(int j = 0; j < distance; j++)
            {
                px = pixelPos.x + i;
                nx = pixelPos.x - i;
                py = pixelPos.y + j;
                ny = pixelPos.y - j;

                CheckClearPixel(px, py);
                CheckClearPixel(nx, py);
                CheckClearPixel(px, ny);
                CheckClearPixel(nx, ny);
            }
        }

        newTexture.Apply();
        MakeSprite();

        if (colHead != null)
        {
            Destroy(colHead);
        }
        colHead = sr_Head.gameObject.AddComponent<PolygonCollider2D>();
        colHead.sharedMaterial = pm_Head;
    }

    private void CheckClearPixel(int x, int y)
    {
        //IMPORTANT CHECK
        if(y> pixelHeight)
        {
            return;
        }
        newTexture.SetPixel(x, y, Color.clear);
    }

    #endregion

    #region CommonAction
    //Create A Sprite
    void MakeSprite()
    {
        sr_Head.sprite = Sprite.Create(newTexture, new Rect(0, 0, newTexture.width, newTexture.height), Vector2.one * 0.5f);
    }

    //Convert To Pixel
    private Vector2Int WorldToPixel(Vector3 pos)
    {
        Vector2Int pixelPosition = Vector2Int.zero;

        var dx = pos.x - sr_Head.transform.position.x;
        var dy = pos.y - sr_Head.transform.position.y;

        pixelPosition.x = Mathf.RoundToInt(0.5f * pixelWidth + dx * (pixelWidth / worldWidth));
        pixelPosition.y = Mathf.RoundToInt(0.5f * pixelHeight + dy * (pixelHeight / worldHeight));

        return pixelPosition;
    }
    #endregion

    #region BornAction
    public void BornTadpole(Vector2 pos)
    {
        GameObject objTadpole = GameObject.Instantiate(pfTadpole, pos, Quaternion.Euler(Vector2.zero), tfContentTadpole);
        TadpoleManager itemTadpole = objTadpole.GetComponent<TadpoleManager>();
        itemTadpole.InitTadpole();
    }

    #endregion

    #region Check

    public int CalculateScore()
    {
        int score = 0;
        ContactFilter2D filter = new ContactFilter2D().NoFilter();
        List<Collider2D> results = new List<Collider2D>();
        triggerHead.OverlapCollider(filter, results);
        foreach(Collider2D col in results)
        {
            if(col.gameObject.tag == "Score")
            {
                score++;
            }
        }
        return score;
    }
    #endregion
}
