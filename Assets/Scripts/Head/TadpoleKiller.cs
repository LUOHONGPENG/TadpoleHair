using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TadpoleKiller : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Tadpole"))
        {
            Destroy(collision.transform.parent.gameObject);
        }
    }


}
