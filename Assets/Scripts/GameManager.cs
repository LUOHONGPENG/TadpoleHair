using LootLocker.Requests;
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
    public bool isGameOver = false;

    public void Start()
    {
        StartCoroutine(LoginRoutine());


        headManager.Init();
        uiManager.Init();
        soundManager.Init();
        isStartGame = false;
        Time.timeScale = 0;
    }

    IEnumerator LoginRoutine()
    {
        bool done = false;
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
            {
                Debug.Log("Player was logged in");
                PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
                done = true;
            }
            else
            {
                Debug.Log("Could not start session");
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }

    public void StartHead()
    {
        Time.timeScale = 1f;
        currentDoubt = 0;
        timerOneTurn = 60f;
        headManager.StartGame();
        uiManager.headUIManager.ShowContent();
        isStartGame = true;
        isGameOver = false;

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
        isStartGame = false;
        if (!isGameOver)
        {
            StartCoroutine(IE_GameOver(isWin));
            isGameOver = true;
        }
    }

    public IEnumerator IE_GameOver(bool isWin)
    {
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0;
        uiManager.endUIManager.ShowContent(isWin);
    }

    private void Update()
    {
        if (isStartGame)
        {
            timerOneTurn -= Time.deltaTime;
            timerCheckScore -= Time.deltaTime;

            if (timerOneTurn < 0)
            {
                currentScore = headManager.CalculateScore();
                GameOver(true);
            }

            //CheckScore
            if (timerCheckScore < 0)
            {
                currentScore = headManager.CalculateScore();
            }
        }
    }
}
