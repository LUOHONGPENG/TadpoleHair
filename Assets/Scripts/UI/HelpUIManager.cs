using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpUIManager : MonoBehaviour
{
    public GameObject objContent;
    public Button btnClose;

    public void Init()
    {
        btnClose.onClick.RemoveAllListeners();
        btnClose.onClick.AddListener(delegate ()
        {
            HideContent();
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
