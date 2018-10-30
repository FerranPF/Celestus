using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    private int enemyHealth = 100;
    public GameObject enemy;
    
    private void Update()
    {
        if (enemyHealth == 0)
        {
            enemy.SetActive(false);
        }
    }

    public void GetDamage(int damage)
    {
        enemyHealth -= damage;
        Debug.Log("Enemy health: " + enemyHealth);
    }
}
