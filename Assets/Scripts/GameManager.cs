using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public HeadManager headManager;
    public UIManager uiManager;


    public void Start()
    {
        headManager.Init();
        uiManager.Init();
    }

    public void StartHead()
    {
        headManager.StartGame();
        uiManager.headUIManager.ShowContent();
    }
}
