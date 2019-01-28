using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotionScript : MonoBehaviour {

    public float healthRecovery;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            PlayerStats playerHealth;
            playerHealth = other.GetComponent<PlayerStats>();
            playerHealth.HealthRecovery(healthRecovery);
            playerHealth.healthPotions++;
            Destroy(gameObject);
        }
    }
}
