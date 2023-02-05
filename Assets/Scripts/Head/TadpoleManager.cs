using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TadpoleManager : MonoBehaviour
{
    [Header("Asset")]
    public SpriteRenderer srBody;
    public Transform tfFace;
    public SpriteRenderer srEyeL;
    public SpriteRenderer srEyeR;
    public SpriteRenderer srMouth;
    public Collider2D colliderTadpole;
    public Animator aniTadpole;

    [Header("Asset")]
    public List<Sprite> listSpBody;
    public List<Sprite> listSpEyeL;
    public List<Sprite> listSpEyeR;
    public List<Sprite> listSpMouth;

    public List<Vector2> listPosBody;
    public List<Vector2> listPosFace;
    public List<Vector2> listPosEyeL;
    public List<Vector2> listPosEyeR;
    public List<Vector2> listPosMouth;

    private PolygonCollider2D colliderPoly;
    private int idBody = 0;
    private int idEye = 0;
    private int idMouth = 0;
    private Coroutine layerCoroutine;

    public float GetTadpoleScale()
    {
        return 0.2f;
    }


    public void InitTadpole()
    {
        InitTadpoleID();
        InitTadpoleSpritePos();
        ResetTadpoleScale();
        layerCoroutine = StartCoroutine(IE_InitTadpole());
    }

    public void InitTadpoleID()
    {
        //idBody = Random.Range(0, 3);
        idBody = 0;
        idEye = Random.Range(0, 3);
        idMouth = Random.Range(0, 3);
    }

    public void InitTadpoleSpritePos()
    {
        srBody.sprite = listSpBody[idBody];
        switch (idBody)
        {
            case 0:
                aniTadpole.Play("aniTad01",0,-1);
                break;
            case 1:
                aniTadpole.Play("aniTad02", 0, -1);
                break;
            case 2:
                aniTadpole.Play("aniTad03", 0, -1);
                break;
        }


        srEyeL.sprite = listSpEyeL[idEye];
        srEyeR.sprite = listSpEyeR[idEye];
        srMouth.sprite = listSpMouth[idMouth];

        tfFace.transform.localPosition = listPosFace[idBody];
        srEyeL.transform.localPosition = listPosEyeL[idEye];
        srEyeR.transform.localPosition = listPosEyeR[idEye];
        srMouth.transform.localPosition = listPosMouth[idMouth];
    }

    public void ResetTadpoleScale()
    {
        srBody.transform.localScale = new Vector2(GetTadpoleScale(), GetTadpoleScale());

        srBody.transform.localPosition = new Vector2(listPosBody[idBody].x * GetTadpoleScale(),listPosBody[idBody].y * GetTadpoleScale());
        //srBody.gameObject.AddComponent<PolygonCollider2D>();
    }

    public IEnumerator IE_InitTadpole()
    {
        yield return new WaitForSeconds(2f);
        srBody.sortingOrder = 0;
        srEyeL.sortingOrder = 1;
        srEyeR.sortingOrder = 1;
        srMouth.sortingOrder = 1;
    }

    public void DestroyReady()
    {
        StopCoroutine(layerCoroutine);
    }
}
