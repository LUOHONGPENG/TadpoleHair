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
    public int currentDoubt = 0;
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
        Time.timeScale = 1f;
        currentDoubt = 0;
        timerOneTurn = 60f;
        headManager.StartGame();
        uiManager.headUIManager.ShowContent();
        isStartGame = true;
    }

    public void AddDoubt(int value)
    {
        currentDoubt += value;
        uiManager.headUIManager.UpdateAngerBar();
        if (currentDoubt >= 100)
        {
            currentDoubt = 100;
            GameOver(false);
        }
    }

    public void GameOver(bool isWin)
    {
        uiManager.endUIManager.ShowContent();
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
                GameOver(true);
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
