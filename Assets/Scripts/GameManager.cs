using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public HeadManager headManager;
    public UIManager uiManager;
    public SoundManager soundManager;

    public float timerOneTurn = 0;
    public bool isStartGame = false;

    public void Start()
    {
        headManager.Init();
        uiManager.Init();
        soundManager.Init();
        isStartGame = false;
    }

    public void StartHead()
    {
        headManager.StartGame();
        uiManager.headUIManager.ShowContent();
        timerOneTurn = 60f;
        isStartGame = true;
    }

    private void Update()
    {
        if (isStartGame)
        {
            timerOneTurn -= Time.deltaTime;
            if (timerOneTurn < 0)
            {

            }
        }
    }
}
