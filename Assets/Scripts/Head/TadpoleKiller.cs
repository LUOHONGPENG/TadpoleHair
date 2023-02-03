using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TadpoleKiller : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Tadpole"))
        {
            GameManager.Instance.soundManager.PlaySound(SoundType.KillTadpole);
            Destroy(collision.transform.parent.gameObject);
        }
    }


}
