using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GhostManager : MonoBehaviour
{
    public void Init(Vector2 pos)
    {
        this.transform.DOMoveY(10F, 5F);


        Destroy(this.gameObject, 10f);
    }

}
