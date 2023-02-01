using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadManager : MonoBehaviour
{
    [Header("Asset")]
    public Texture2D texture_Head;
    public SpriteRenderer sr_Head;
    public PhysicsMaterial2D pm_Head;
    public GameObject pfTadpole;
    public Transform tfContent;
    //Generate Dynamically
    private Texture2D newTexture;
    private PolygonCollider2D colHead;

    //Some mapping information
    float worldWidth, worldHeight;
    float pixelWidth, pixelHeight;
    //Cool down timer for generating the tadpole
    float timerBornTadpole = 0;

    private void Start()
    {
        StartPlanting();
    }

    public void Init()
    {

    }

    public void StartPlanting()
    {
        InitHead();
    }


    #region Init
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
        colHead = sr_Head.gameObject.AddComponent<PolygonCollider2D>();
        colHead.sharedMaterial = pm_Head;
    }
    #endregion

    #region Dot

    public void Update()
    {
        timerBornTadpole -= Time.deltaTime;
        //
        CheckMakingDot();
        if (timerBornTadpole < 0)
        {
            CheckCreatingTadpole();
        }
    }

    public void CheckMakingDot()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Collider2D overCollider2D = Physics2D.OverlapCircle(MousePosition, 0.01f,LayerMask.GetMask("Head"));
            if (overCollider2D != null)
            {
                MakeDot(MousePosition);
            }
        }
    }

    public void CheckCreatingTadpole()
    {
        if (Input.GetMouseButton(1))
        {
            Vector3 MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            CreateTadpole(new Vector2(MousePosition.x, MousePosition.y));

            timerBornTadpole = 2f;
        }
    }

    //When click the pixel, make a dot
    public void MakeDot(Vector3 pos)
    {
        Vector2Int pixelPos = WorldToPixel(pos);
        Debug.Log(pixelPos);
        int radius = 25;

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

                newTexture.SetPixel(px, py, Color.clear);
                newTexture.SetPixel(nx, py, Color.clear);
                newTexture.SetPixel(px, ny, Color.clear);
                newTexture.SetPixel(nx, ny, Color.clear);
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

    void MakeSprite()
    {
        sr_Head.sprite = Sprite.Create(newTexture, new Rect(0, 0, newTexture.width, newTexture.height), Vector2.one * 0.5f);
    }

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


    #region CheckCreateTadpole

    public void CreateTadpole(Vector2 pos)
    {
        GameObject objTadpole = GameObject.Instantiate(pfTadpole, pos,Quaternion.Euler(Vector2.zero), tfContent);

    }

    #endregion
}
