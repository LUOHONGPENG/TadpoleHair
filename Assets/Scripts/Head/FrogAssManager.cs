using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FrogAssManager : MonoBehaviour
{
    public GameObject objContent;
    public Transform tfAss;
    public SpriteRenderer srAss;

    private HeadManager parent;



    #region Basic
    public void Init(HeadManager parent)
    {
        this.parent = parent;
    }

    public void ShowContent()
    {
        objContent.SetActive(true);
    }

    public void HideContent()
    {
        objContent.SetActive(false);
    }

    public void SetPosition(Vector2 pos)
    {
        this.transform.position = new Vector2(pos.x, GameGlobal.PosAssY);
    }

    public Vector2 GetAssPosition()
    {
        return tfAss.position;
    }
    #endregion

    #region DotweenAni

    public IEnumerator IE_bornAni()
    {
        float initialPosY = srAss.transform.localPosition.y;
        srAss.transform.DOLocalMoveY(initialPosY + 0.3f,GameGlobal.intervalBorn/2f);
        yield return new WaitForSeconds(GameGlobal.intervalBorn / 2f);
        srAss.transform.DOLocalMoveY(initialPosY, GameGlobal.intervalBorn / 2f);
        yield break;
    }
    #endregion
}
