using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPotionScript : MonoBehaviour {

    public float manaRecovery;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerHealth playerHealth;
            playerHealth = other.GetComponent<PlayerHealth>();
            playerHealth.ManaRecovery(manaRecovery);
            playerHealth.manaPotions ++;
            Destroy(gameObject);
        }
    }
}
