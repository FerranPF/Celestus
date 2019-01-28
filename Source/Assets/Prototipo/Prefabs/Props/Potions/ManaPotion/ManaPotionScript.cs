using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPotionScript : MonoBehaviour {

    public float manaRecovery;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerStats playerHealth;
            playerHealth = other.GetComponent<PlayerStats>();
            playerHealth.ManaRecovery(manaRecovery);
            playerHealth.manaPotions ++;
            Destroy(gameObject);
        }
    }
}
