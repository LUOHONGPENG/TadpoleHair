using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogAssManager : MonoBehaviour
{
    public GameObject objContent;
    public Transform tfAss;
    public SpriteRenderer srAss;




    #region Basic
    public void ShowContent()
    {
        objContent.SetActive(true);
    }

    public void HideContent()
    {
        objContent.SetActive(false);
    }
    #endregion

    #region Location
    public void SetPosition(Vector2 pos)
    {
        this.transform.position = new Vector2(pos.x, 3.5f);
    }

    public Vector2 GetAssPosition()
    {
        return tfAss.position;
    }
    #endregion
}
