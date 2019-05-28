using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutoEvent : MonoBehaviour
{
    public string keyText;
    public Text eventText;
    public float timeStopped = 0.2f;
    public bool open = false;
    private bool showed = false;

    private PlayerController playerCont;
    private TutoDoor doorController;

    private void Awake()
    {
        playerCont = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().playerController;
        doorController = GetComponentInParent<TutoDoor>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!showed)
        {
            if (other.gameObject.tag == "Player")
            {
                StartCoroutine(StopPlayer());
            }
        }
    }

    IEnumerator StopPlayer()
    {
        PlayerKey();
        Time.timeScale = 0.0f;
        playerCont.canMove = false;
        showed = true;

        yield return new WaitForSecondsRealtime(timeStopped);

        playerCont.canMove = true;
        eventText.enabled = false;
        Time.timeScale = 1.0f;
    }

    void PlayerKey()
    {
        eventText.enabled = true;
        if (open)
        {
            doorController.OpenDoor();
        }
        else
        {
            doorController.CloseDoor();
        }
        
        eventText.text = keyText;
    }
}
