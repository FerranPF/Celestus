using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour
{
    public void AttackEvent()
    {
        PlayerController playerCont;
        playerCont = GetComponentInParent<PlayerController>();

        playerCont.AttackOverlap();
        Debug.Log("AttackEvent");
    }

    public void EnemyAttackEvent()
    {
        EnemyController enemyCont;
        enemyCont = GetComponentInParent<EnemyController>();

        enemyCont.AttackOverlap();
        Debug.Log("AttackEvent");
    }

}
