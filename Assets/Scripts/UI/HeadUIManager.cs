using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadUIManager : MonoBehaviour
{
    public GameObject objContent;
    public Button btnTongue;
    public Button btnTodpole;
    public Button btnFrogEgg;
    public Button btnWater;

    public Text txTime;
    public Text txScore;


    private HeadManager headManager;

    private void Update()
    {
        if (GameManager.Instance.isStartGame)
        {
            txTime.text = Mathf.CeilToInt(GameManager.Instance.timerOneTurn).ToString();
            txScore.text = GameManager.Instance.currentScore.ToString();

            switch (headManager.getActionType())
            {
                case ActionType.Lick:
                    btnTongue.transform.localScale = new Vector3(1.2F, 1.2F,1f);
                    btnTodpole.transform.localScale = new Vector3(0.9F, 0.9F,1f);
                    break;
                case ActionType.Tadpole:
                    btnTongue.transform.localScale = new Vector3(0.9F,0.9F,1f);
                    btnTodpole.transform.localScale = new Vector3(1.2F, 1.2F,1f);
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
