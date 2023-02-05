using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public StartPageManager startPageManager;
    public HeadUIManager headUIManager;
    public HelpUIManager helpUIManager;
    public EndUIManager endUIManager;
    public void Init()
    {
        //InitAllUIManager;
        startPageManager.Init();
        headUIManager.Init();
        helpUIManager.Init();
        endUIManager.Init();
        //
        startPageManager.ShowContent();
    }
}
