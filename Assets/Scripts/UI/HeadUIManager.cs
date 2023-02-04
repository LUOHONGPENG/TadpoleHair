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
    public Image imgAnger;
    public List<Sprite> listSpAnger;
    public Color startColor;
    public Color endColor;

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
        UpdateAngerBar();
    }

    public void HideContent()
    {
        objContent.SetActive(false);
    }


    #region AngerBar

    public void UpdateAngerBar()
    {
        //imgAlarm.fillAmount = GameManager.Instance.currentDoubt / 100f;
        float rate = GameManager.Instance.currentDoubt / 100f;
        imgAlarm.DOFillAmount(rate, 0.2f);
        imgAnger.transform.DOLocalMove(CalculatePosAnger(rate), 0.2f);
        imgAlarm.DOColor(CalculateColorAnger(rate), 0.2f);
        if (rate >= 0.8F)
        {
            imgAnger.sprite = listSpAnger[2];
        }
        else if(rate >= 0.4f)
        {
            imgAnger.sprite = listSpAnger[1];
        }
        else
        {
            imgAnger.sprite = listSpAnger[0];
        }
        imgAnger.SetNativeSize();
    }

    public Vector2 CalculatePosAnger(float rate)
    {
        float posY = -500f + rate * (587f - (-500f));
        float posX = -10f + rate * (0 - (-10f));
        return new Vector2(posX,posY);
    }

    public Color CalculateColorAnger(float rate)
    {
        float colorR = startColor.r + rate * (endColor.r - startColor.r);
        float colorG = startColor.g + rate * (endColor.g - startColor.g);
        float colorB = startColor.b + rate * (endColor.b - startColor.b);
        return new Color(colorR, colorG,colorB,1f);
    }
    #endregion

}
