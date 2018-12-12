using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotionScript : MonoBehaviour {

    public float healthRecovery;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            PlayerHealth playerHealth;
            playerHealth = other.GetComponent<PlayerHealth>();
            playerHealth.HealthRecovery(healthRecovery);
            playerHealth.healthPotions++;
            Destroy(gameObject);
        }
    }
}
