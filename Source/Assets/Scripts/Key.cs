using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public int keyIndex;
    public AudioSource audioSource;
    public AudioClip audioKey;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            audioSource.clip = audioKey;
            audioSource.Play(0);
            other.gameObject.GetComponent<PlayerStats>().Key(keyIndex);
            Destroy(this.gameObject);
        }
    }
}
