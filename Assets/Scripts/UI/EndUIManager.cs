using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndUIManager : MonoBehaviour
{
    public enum PageType
    {
        Normal,
        Upload,
        Board
    }


    [Header("Content")]
    public GameObject objContent;
    public GameObject contentNormal;
    public GameObject contentUpload;
    public GameObject contentBoard;
    [Header("Normal")]
    public Text txScore;
    public Text codeScore;
    public Text txWaste;
    public Image imgFail;
    public Image imgWin;
    public Button btnRestart;
    public Button btnUpload;
    public Button btnBoard;
    [Header("Upload")]
    public Text codeScoreUpload;
    public Button btnBackFromLoad;
    public Button btnRealUpload;
    [Header("Board")]
    public Button btnBackFromBoard;

    private bool isUpload = false;

    public void Init()
    {
        btnRestart.onClick.RemoveAllListeners();
        btnRestart.onClick.AddListener(delegate ()
        {
            GameManager.Instance.StartHead();
            HideContent();
        });

        btnUpload.onClick.RemoveAllListeners();
        btnUpload.onClick.AddListener(delegate ()
        {
            GoPage(PageType.Upload);
        });

        btnBoard.onClick.RemoveAllListeners();
        btnBoard.onClick.AddListener(delegate ()
        {
            GoPage(PageType.Board);
        });

        btnRealUpload.onClick.RemoveAllListeners();
        btnRealUpload.onClick.AddListener(delegate ()
        {

        });

        btnBackFromLoad.onClick.RemoveAllListeners();
        btnBackFromLoad.onClick.AddListener(delegate ()
        {
            GoPage(PageType.Normal);
        });

        btnBackFromBoard.onClick.RemoveAllListeners();
        btnBackFromBoard.onClick.AddListener(delegate ()
        {
            GoPage(PageType.Normal);
        });
    }

    public void GoPage(PageType type)
    {
        contentNormal.SetActive(false);
        contentUpload.SetActive(false);
        contentBoard.SetActive(false);
        switch (type)
        {
            case PageType.Normal:
                contentNormal.SetActive(true);
                if (isUpload)
                {
                    btnUpload.gameObject.SetActive(false);
                }
                break;
            case PageType.Upload:
                contentUpload.SetActive(true);
                break;
            case PageType.Board:
                contentBoard.SetActive(true);
                break;
        }
        
    }

    public void ShowContent(bool isWin)
    {
        StartCoroutine(IE_ShowContent(isWin));
    }

    public IEnumerator IE_ShowContent(bool isWin)
    {
        yield return new WaitForEndOfFrame();
        if (isWin)
        {
            RenderTexture rt = new RenderTexture(Screen.width, Screen.height, 0);
            Camera.main.targetTexture = rt;
            Camera.main.Render();

            RenderTexture.active = rt;

            Texture2D screenShot = new Texture2D(Screen.width,Screen.height, TextureFormat.RGB24, false);
            screenShot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            screenShot.Apply();

            Camera.main.targetTexture = null;
            RenderTexture.active = null;
            Destroy(rt);

            Sprite sr = Sprite.Create(screenShot, new Rect(0, 0, Screen.width, Screen.height), new Vector2(0.5F,0.5F));
            imgWin.sprite = sr;
        }

        InitContent(isWin);
    }

    public void InitContent(bool isWin)
    {
        objContent.SetActive(true);
        isUpload = false;
        GoPage(PageType.Normal);
        btnRestart.gameObject.SetActive(true);
        if (isWin)
        {
            txScore.gameObject.SetActive(true);
            txWaste.gameObject.SetActive(false);
            codeScore.text = GameManager.Instance.currentScore.ToString();
            codeScoreUpload.text = GameManager.Instance.currentScore.ToString();
            imgFail.gameObject.SetActive(false);
            imgWin.gameObject.SetActive(true);
            btnUpload.gameObject.SetActive(true);
            btnBoard.gameObject.SetActive(true);
        }
        else
        {
            txScore.gameObject.SetActive(false);
            txWaste.gameObject.SetActive(true);
            imgFail.gameObject.SetActive(true);
            imgWin.gameObject.SetActive(false);
            btnUpload.gameObject.SetActive(false);
            btnBoard.gameObject.SetActive(false);
        }
    }

    public void HideContent()
    {
        objContent.SetActive(false);
    }



}
