using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogMouthManager : MonoBehaviour
{
    public GameObject objContent;
    public Transform tfMouth;
    public Transform tfTongue;

    #region Basic
    public void ShowContent()
    {
        objContent.SetActive(true);
        tfTongue.transform.localPosition = new Vector2(0,3.8f);
    }

    public void HideContent()
    {
        objContent.SetActive(false);
    }
    #endregion

    #region Tongue

    public void ExtendTongue()
    {

    }

    #endregion
}
