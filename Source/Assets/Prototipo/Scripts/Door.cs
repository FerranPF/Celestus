using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    public Animator anim;
    public int key;
    public AudioSource audioSource;
    public AudioClip audioOpen;
    public AudioClip audioNoKey;

    private void Awake()
    {

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
                audioSource.clip = audioNoKey;
                audioSource.Play(0);
            }
        }
    }

}
