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

    public void InitTadpole()
    {
        int ranBody = Random.Range(0, 3);
        int ranEye = Random.Range(0, 3);
        int ranMouth = Random.Range(0, 3);

        srBody.transform.localScale = new Vector2(0.3f, 0.3f);

        srBody.sprite = listSpBody[ranBody];
        srEyeL.sprite = listSpEyeL[ranEye];
        srEyeR.sprite = listSpEyeR[ranEye];
        srMouth.sprite = listSpMouth[ranMouth];

        srBody.transform.localPosition = listPosBody[ranBody];
        tfFace.transform.localPosition = listPosFace[ranBody];
        srEyeL.transform.localPosition = listPosEyeL[ranEye];
        srEyeR.transform.localPosition = listPosEyeR[ranEye];
        srMouth.transform.localPosition = listPosMouth[ranMouth];

        //srBody.gameObject.AddComponent<PolygonCollider2D>();
    }

}
