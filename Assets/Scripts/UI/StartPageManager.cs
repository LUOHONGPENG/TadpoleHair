using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPageManager : MonoBehaviour
{
    public GameObject objPage;
    public Transform tfFrog;
    public Button btnPlay;
    public Button btnHelp;

    public void Init()
    {
        btnPlay.onClick.RemoveAllListeners();
        btnPlay.onClick.AddListener(delegate ()
        {
            StartGame();
        });

        btnHelp.onClick.RemoveAllListeners();
        btnHelp.onClick.AddListener(delegate ()
        {

        });
    }

    public void Update()
    {
        if (objPage.activeSelf)
        {
            Vector2 mousePos = PublicTool.GetMousePosition2D();

            tfFrog.position = new Vector2(0.4f * mousePos.x,0.4f * mousePos.y);
        }
    }

    public void ShowContent()
    {
        objPage.SetActive(true);
    }

    public void StartGame()
    {
        objPage.SetActive(false);
        GameManager.Instance.StartHead();
    }


}
