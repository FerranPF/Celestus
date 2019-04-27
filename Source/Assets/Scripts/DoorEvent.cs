using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorEvent : MonoBehaviour
{
    public string noKeyText;
    public string keyText;
    public Text eventText;
    public float timeStopped = 0.2f;
    public float timeToAsk = 1.0f;

    public bool hasStopped = false;
    public bool playerExits = true;

    private PlayerStats playerStats;
    private PlayerController playerCont;

    private float cont = 0.0f;

    private void Awake()
    {
        playerStats = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().playerStats;
        playerCont = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().playerController;
    }

    private void Update()
    {
        if (hasStopped && playerExits)
        {
            cont += Time.deltaTime;
            if(cont >= timeToAsk)
            {
                hasStopped = false;
                cont = 0.0f;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!hasStopped)
            {
                StartCoroutine(StopPlayer());
            }
            eventText.text = keyText;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            hasStopped = true;
            playerExits = true;
        }
    }

    IEnumerator StopPlayer()
    {
        PlayerKey();
        Time.timeScale = 0.0f;
        playerCont.canMove = false;

        yield return new WaitForSecondsRealtime(timeStopped);

        playerCont.canMove = true;
        playerExits = false;
        hasStopped = true;
        eventText.enabled = false;
        Time.timeScale = 1.0f;
    }

    void PlayerKey()
    {
        eventText.enabled = true;
        switch (playerStats.key)
        {
            case true:
                eventText.text = keyText;
                break;
            case false:
                eventText.text = noKeyText;
                break;
        }
    }

}
