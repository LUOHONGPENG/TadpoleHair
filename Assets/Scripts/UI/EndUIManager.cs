using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LootLocker.Requests;
using System;

public class EndUIManager : MonoBehaviour
{
    public enum PageType
    {
        Normal,
        Upload,
        Board
    }

    int leaderboardID = 11400;

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
    public Text txTipTypeName;
    public InputField inputName;
    public Button btnBackFromLoad;
    public Button btnRealUpload;
    [Header("Board")]
    public Transform tfContentLeaderBoard;
    public GameObject pfLeaderBoard;
    public Button btnBackFromBoard;

    private bool isUpload = false;

    [System.Obsolete]
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
            if (inputName.text.Length > 0 && inputName.text.Length < 8 && CheckAllCanASCII(inputName.text))
            {
                SetPlayerName();
                StartCoroutine(SubmitScoreRoutine(GameManager.Instance.currentScore, PlayerPrefs.GetString("PlayerID")));
                isUpload = true;
                GoPage(PageType.Normal);
            }
            else
            {
                txTipTypeName.gameObject.SetActive(true);
            }
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

    [Obsolete]
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
                InitLeaderBoard();
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

    #region AboutLeaderBoard

    public void SetPlayerName()
    {
        LootLockerSDKManager.SetPlayerName(inputName.text, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Successfully set player name");
            }
            else
            {
                Debug.Log("Could not set player name");
            }
        });
    }

    public long GetUnixTime()
    {
        DateTime now = DateTime.Now;
        DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 0, 0, 0, 0));
        return (long)Math.Round((now - startTime).TotalMilliseconds / 1000, MidpointRounding.AwayFromZero);
    }

    public long GetASCII(string str)
    {
        long num = 1;
        for(int i = 0; i < str.Length; i++)
        {
            num = num * 100 + (int)str[i];
        }
        return num;
    }

    public bool CheckAllCanASCII(string str)
    {
        for (int i = 0; i < str.Length; i++)
        {
            if ('0' <= str[i] && str[i] <= '9')
            {
                continue;
            }
            if ('a' <= str[i] && str[i] <= 'z')
            {
                continue;
            }

            if ('A' <= str[i] && str[i] <= 'Z')
            {
                continue;
            }
            return false;
        }
        return true;
    }


    [System.Obsolete]
    public IEnumerator SubmitScoreRoutine(float time, string playerName)
    {
        bool done = false;
        //string nowTime = GetUnixTime().ToString();
        string memberID = GetASCII(inputName.text).ToString();
        
        int score = GameManager.Instance.currentScore;
        LootLockerSDKManager.SubmitScore(memberID, score, leaderboardID, inputName.text, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Successfully uploaded time");
                done = true;
            }
            else
            {
                Debug.Log("Failed" + response.Error);
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }

    [System.Obsolete]
    public void InitLeaderBoard()
    {
        PublicTool.ClearChildItem(tfContentLeaderBoard);
        StartCoroutine(FetchLeaderBoardRoutine());
    }

    [System.Obsolete]

    public IEnumerator FetchLeaderBoardRoutine()
    {
        bool done = false;
        LootLockerSDKManager.GetScoreListMain(leaderboardID, 100, 0, (response) =>
        {
            if (response.success)
            {
                LootLockerLeaderboardMember[] members = response.items;

                for (int i = 0; i < members.Length; i++)
                {
                    GameObject objMember = GameObject.Instantiate(pfLeaderBoard, tfContentLeaderBoard);
                    ItemLeaderBoard itemMember = objMember.GetComponent<ItemLeaderBoard>();
                    itemMember.Init(i + 1, members[i].metadata, members[i].score);
                }
                done = true;
            }
            else
            {
                Debug.Log("Failed" + response.Error);
                done = true;
            }

        });
        yield return new WaitWhile(() => done == false);
    }

    #endregion

}
