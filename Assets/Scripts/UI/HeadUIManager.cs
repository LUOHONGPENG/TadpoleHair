using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HeadUIManager : MonoBehaviour
{
    public GameObject objContent;
    public Button btnTongue;
    public Button btnTodpole;
    public Button btnFrogEgg;
    public Button btnWater;
    [Header("Data")]
    public Text txTime;
    public Text txScore;
    public Image imgAlarm;

    private HeadManager headManager;

    private void Update()
    {
        if (GameManager.Instance.isStartGame)
        {
            txTime.text = Mathf.CeilToInt(GameManager.Instance.timerOneTurn).ToString();
            txScore.text = GameManager.Instance.currentScore.ToString();

            imgAlarm.fillAmount = GameManager.Instance.currentDoubt / 100f;

            switch (headManager.getActionType())
            {
                case ActionType.Lick:
                    LickButtonAni();
                    //btnTongue.transform.localScale = new Vector3(1.2F, 1.2F,1f);
                    //btnTodpole.transform.localScale = new Vector3(0.7F, 0.7F,1f);
                    break;
                case ActionType.Tadpole:
                    BornButtonAni();
                    //btnTongue.transform.localScale = new Vector3(0.7F,0.7F,1f);
                    //btnTodpole.transform.localScale = new Vector3(1.2F, 1.2F,1f);
                    break;
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                headManager.ChangeAction(ActionType.Lick);
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                headManager.ChangeAction(ActionType.Tadpole);
            }
        }
    }

    public void LickButtonAni()
    {
        btnTongue.transform.DOScale(1.2f, 0.2f);
        btnTodpole.transform.DOScale(0.7f, 0.2f);

    }

    public void BornButtonAni()
    {
        btnTongue.transform.DOScale(0.7f, 0.2f);
        btnTodpole.transform.DOScale(1.2f, 0.2f);
    }

    public void Init()
    {
        headManager = GameManager.Instance.headManager;

        btnTongue.onClick.RemoveAllListeners();
        btnTongue.onClick.AddListener(delegate ()
        {
            headManager.ChangeAction(ActionType.Lick);
        });

        btnTodpole.onClick.RemoveAllListeners();
        btnTodpole.onClick.AddListener(delegate ()
        {
            headManager.ChangeAction(ActionType.Tadpole);
        });

        btnFrogEgg.onClick.RemoveAllListeners();
        btnFrogEgg.onClick.AddListener(delegate ()
        {
            headManager.ChangeAction(ActionType.FrogEgg);
        });

        btnWater.onClick.RemoveAllListeners();
        btnWater.onClick.AddListener(delegate ()
        {
            headManager.ChangeAction(ActionType.Water);
        });
    }

    public void ShowContent()
    {
        objContent.SetActive(true);
    }

    public void HideContent()
    {
        objContent.SetActive(false);
    }
}
