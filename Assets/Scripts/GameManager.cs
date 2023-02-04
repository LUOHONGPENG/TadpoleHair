using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public HeadManager headManager;
    public UIManager uiManager;
    public SoundManager soundManager;

    public float timerOneTurn = 0;
    public float timerCheckScore = 0;
    public int currentScore = 0;
    public bool isStartGame = false;

    public void Start()
    {
        headManager.Init();
        uiManager.Init();
        soundManager.Init();
        isStartGame = false;
        Time.timeScale = 0;
    }

    public void StartHead()
    {
        headManager.StartGame();
        uiManager.headUIManager.ShowContent();
        Time.timeScale = 1f;
        timerOneTurn = 60f;
        isStartGame = true;
    }

    private void Update()
    {
        if (isStartGame)
        {
            timerOneTurn -= Time.deltaTime;
            timerCheckScore -= Time.deltaTime;

            if (timerOneTurn < 0)
            {
                Time.timeScale = 0;
                currentScore = headManager.CalculateScore();
                uiManager.endUIManager.ShowContent();
            }

            //CheckScore
            if (timerCheckScore < 0)
            {
                currentScore = headManager.CalculateScore();
                Debug.Log(currentScore);
            }
        }
    }
}
