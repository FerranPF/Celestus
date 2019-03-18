using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour
{
    PlayerController playerCont;

    private void Update()
    {
        playerCont = GetComponentInParent<PlayerController>();
    }

    public void AttackEvent()
    {
        playerCont.AttackOverlap();
        Debug.Log("AttackEvent");
    }

}
