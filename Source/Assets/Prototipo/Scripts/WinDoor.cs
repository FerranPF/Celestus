using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinDoor : MonoBehaviour
{
    GameManager manager;
    public Animator anim;
    public AudioSource audioSource;
    public AudioClip audioNoKey;

    private void Awake()
    {
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerStats player = other.gameObject.GetComponent<PlayerStats>();
            if (player.win)
            {
                anim.SetBool("open", true);
                manager.Win();
            }
            else
            {

                audioSource.clip = audioNoKey;
                audioSource.Play(0);
            }
        }
    }
}
