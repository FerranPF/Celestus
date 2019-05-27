using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySword : MonoBehaviour
{
    public bool m_Started;
    public int minDamage = 10;
    public int maxDamage = 15;

    public void MyCollision()
    {
        Collider[] playerColliders = Physics.OverlapBox(transform.position, transform.localScale, Quaternion.LookRotation(Vector3.forward, Vector3.up));
        int i = 0;

        while (i < playerColliders.Length)
        {
            Debug.Log("Hit : " + playerColliders[i].name + i);
            if (playerColliders[i].tag == "Player")
            {
                PlayerStats player = playerColliders[i].GetComponent<PlayerStats>();
                player.TakeDamage(Random.Range(minDamage, maxDamage));
            }

            i++;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (m_Started)
            Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
