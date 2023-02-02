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

    private HeadManager headManager;

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
