using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour {
    
    public int damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerStats player = other.GetComponent<PlayerStats>();
            if(player.currentHealth >= damage)
            {
                player.TakeDamage(damage);
            }
        }
    }
}
