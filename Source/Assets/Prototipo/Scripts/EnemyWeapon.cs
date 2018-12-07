using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour {
    
    public int damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerHealth player = other.GetComponent<PlayerHealth>();
            player.TakeDamage(damage);
        }
    }
}
