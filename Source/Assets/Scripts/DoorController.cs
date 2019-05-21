using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private Animator doorAnim;
    private DoorEvent doorEvent;

    private void Awake()
    {
        doorEvent = GetComponentInChildren<DoorEvent>();
        doorAnim = GetComponent<Animator>();
        doorAnim.enabled = false;
    }

    public void OpenDoor()
    {
        doorAnim.enabled = true;
        doorAnim.SetBool("open", true);
        doorEvent.enabled = false;
    }

    public void CloseDoor()
    {
        doorAnim.SetBool("open", false);
    }

}
