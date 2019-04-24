using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour {
    
    public int damage;
    public BoxCollider coll;
    public AudioSource audioSource;
    public AudioClip hit;

    private void Awake()
    {
        coll = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerStats player = other.GetComponent<PlayerStats>();
            if(player.currentHealth >= damage)
            {
                audioSource.clip = hit;
                audioSource.Play(0);
                Debug.Log("Player damage");
                player.TakeDamage(damage);
                coll.enabled = false;
            }
        }
    }
}
