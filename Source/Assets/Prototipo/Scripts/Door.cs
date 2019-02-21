using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator anim;
    public int key;
    GameManager manager;
    public AudioSource audioSource;
    public AudioClip audioOpen;

    private void Awake()
    {
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerStats player = other.gameObject.GetComponent<PlayerStats>();
            if (player.keys[key] == 1)
            {
                audioSource.clip = audioOpen;
                audioSource.Play(0);
                Debug.Log("Open door");
                anim.SetBool("open", true);
                Destroy(this);
            }
            else
            {
                Debug.Log("Do not have the key");
            }
        }
    }

}
