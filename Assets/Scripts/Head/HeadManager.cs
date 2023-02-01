using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadManager : MonoBehaviour
{
    public Texture2D texture_Head;
    Texture2D newTexture;
    public SpriteRenderer sr_Head;
    
    public GameObject pfTadpole;
    public Transform tfContent;

    float worldWidth, worldHeight;
    float pixelWidth, pixelHeight;

    float timerBornTadpole = 0;

    private void Start()
    {
        Init();
    }



    public void Init()
    {
        CreateHeadCollider();
    }

    #region Init
    public void CreateHeadCollider()
    {
        newTexture = new Texture2D(texture_Head.width, texture_Head.height);
        Color[] colors = texture_Head.GetPixels();
        newTexture.SetPixels(colors);

        newTexture.Apply();
        MakeSprite();

        worldWidth = sr_Head.bounds.size.x;
        worldHeight = sr_Head.bounds.size.y;
        pixelWidth = sr_Head.sprite.texture.width;
        pixelHeight = sr_Head.sprite.texture.height;

        Debug.Log("World:" + worldWidth + "," + worldHeight + "Pixel:" + pixelWidth + "," + pixelHeight);

        sr_Head.gameObject.AddComponent<PolygonCollider2D>();
    }
    #endregion

    #region Dot

    public void Update()
    {
        timerBornTadpole -= Time.deltaTime;

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

        Destroy(sr_Head.gameObject.GetComponent<PolygonCollider2D>());
        sr_Head.gameObject.AddComponent<PolygonCollider2D>();

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
