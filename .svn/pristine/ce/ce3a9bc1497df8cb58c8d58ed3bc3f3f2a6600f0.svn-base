using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HandMove : MonoBehaviour
{
    public Transform handTrans;
    public Transform enemyAttackBall;
    public float Force=0.2f;
    Tweener tweener;

    public void Shield()
    {
        Debug.Log("挥手");
        Vector3 distance = enemyAttackBall.position - handTrans.position;
        tweener = transform.DOMove(enemyAttackBall.position + Force * distance, 2.0f).SetAutoKill(true);
        if(handTrans.position!= enemyAttackBall.position + Force * distance)
        {
            tweener.ChangeEndValue((enemyAttackBall.position + Force * distance), false).Restart();
        }
    }
}
