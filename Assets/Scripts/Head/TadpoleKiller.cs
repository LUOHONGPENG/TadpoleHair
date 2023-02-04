using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TadpoleKiller : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Tadpole")&& collision.gameObject.tag == "Score")
        {
            GameManager.Instance.soundManager.PlaySound(SoundType.KillTadpole);
            TadpoleManager item = collision.transform.parent.parent.parent.GetComponent<TadpoleManager>();
            if (item != null)
            {
                Vector2 posTadpole = new Vector2(item.transform.position.x, item.transform.position.y);
                GameManager.Instance.headManager.CreateGhost(posTadpole);
                item.DestroyReady();
            }
            Destroy(collision.transform.parent.gameObject);

            GameManager.Instance.AddDoubt(GameGlobal.OneTadpoleDropPenalty);
        }
    }


}
