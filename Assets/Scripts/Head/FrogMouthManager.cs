using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FrogMouthManager : MonoBehaviour
{
    public GameObject objContent;
    public Transform tfMouth;
    public Transform tfTongue;
    public CircleCollider2D colTongue;
    public Rigidbody2D bodyTongue;

    private float timerGoDown = 0;
    private float timerGoUp = 0;
    private float speedTongueDown = 0;
    private float speedTongueNormalUp = 0;
    private float speedTongueSpecialUp = 0;
    private bool isNormalLick = true;

    private HeadManager parent;

    private void Update()
    {
        CheckWhetherBreakTongue();
        CheckMoveTongue();
    }

    #region Basic
    public void Init(HeadManager parent)
    {
        this.parent = parent;

        float lengthPre = tfTongue.transform.localPosition.y - GameGlobal.LowestTongue;
        speedTongueDown = lengthPre / (GameGlobal.intervalLick / 2f);
        speedTongueNormalUp = lengthPre / (GameGlobal.intervalLick / 2f);
    }

    public void ShowContent()
    {
        objContent.SetActive(true);
        tfTongue.transform.localPosition = new Vector2(0, GameGlobal.initialTongueY);
    }

    public void HideContent()
    {
        objContent.SetActive(false);
    }

    public void SetPosition(Vector2 pos)
    {
        this.transform.position = new Vector2(pos.x, GameGlobal.PosMouthY);
    }
    #endregion

    #region Tongue

    public void StartExtendTongue()
    {
        //Predict the speed
        timerGoDown = GameGlobal.intervalLick / 2f;
        timerGoUp = GameGlobal.intervalLick / 2f;
        isNormalLick = true;
        speedTongueSpecialUp = 0;
        colTongue.enabled = true;
    }

    public void BreakTongue()
    {
        Debug.Log("Break");
        float holeCenterX = colTongue.transform.position.x;
        float holeCenterY = colTongue.transform.position.y - colTongue.radius;
        parent.MakeHole(new Vector2(holeCenterX,holeCenterY));

        timerGoDown = 0;
        isNormalLick = false;
        float lengthPre = GameGlobal.initialTongueY - tfTongue.transform.localPosition.y;
        speedTongueSpecialUp = lengthPre / (GameGlobal.intervalLick / 2f);

        colTongue.enabled = false;
    }

    private void CheckWhetherBreakTongue()
    {
        if (timerGoDown > 0)
        {
            ContactFilter2D filter = new ContactFilter2D().NoFilter();
            List<Collider2D> results = new List<Collider2D>();
            colTongue.OverlapCollider(filter, results);
            foreach (Collider2D col in results)
            {
                if (col.gameObject.layer == LayerMask.NameToLayer("Head"))
                {
                    BreakTongue();
                }
            }
        }
    }

    private void CheckMoveTongue()
    {
        if (timerGoDown > 0)
        {
            timerGoDown -= Time.deltaTime;
            Vector2 initialPos = tfTongue.transform.localPosition;
            tfTongue.transform.localPosition = new Vector2(initialPos.x, initialPos.y - speedTongueDown * Time.deltaTime);
        }
        else if(timerGoUp > 0)
        {
            timerGoUp -= Time.deltaTime;
            Vector2 initialPos = tfTongue.transform.localPosition;
            if (isNormalLick)
            {
                tfTongue.transform.localPosition = new Vector2(initialPos.x, initialPos.y + speedTongueNormalUp * Time.deltaTime);
            }
            else
            {
                tfTongue.transform.localPosition = new Vector2(initialPos.x, initialPos.y + speedTongueSpecialUp * Time.deltaTime);
            }
        }
        else
        {
            colTongue.enabled = false;
            tfTongue.transform.localPosition = new Vector2(0, GameGlobal.initialTongueY);
        }
    }
    #endregion
}
