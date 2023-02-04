using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndUIManager : MonoBehaviour
{
    public GameObject objContent;
    public Text txScore;
    public Button btnRestart;

    public void Init()
    {
        btnRestart.onClick.RemoveAllListeners();
        btnRestart.onClick.AddListener(delegate ()
        {
            GameManager.Instance.StartHead();
            HideContent();
        });
    }

    public void ShowContent()
    {
        objContent.SetActive(true);
        txScore.text = GameManager.Instance.currentScore.ToString();
    }

    public void HideContent()
    {
        objContent.SetActive(false);
    }


}
