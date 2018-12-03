using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public int enemyHealth = 100;
    private CapsuleCollider enemyCol;
    private Renderer enemyRen;
	
	private GameObject player;
	private PlayerHealth playerHealth;
	
    private void Start()
    {
		player = GameObject.FindGameObjectWithTag("Player");
		playerHealth = player.GetComponent<PlayerHealth>();
		
        enemyRen = GetComponent<Renderer>();
        enemyCol = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        if (enemyHealth == 0)
        {
            Death();
        }
    }

    public void GetDamage(int damage)
    {
        enemyHealth -= damage;
        Debug.Log("Enemy health: " + enemyHealth);
    }

    protected void Death()
    {
        enemyRen.enabled = false;
        enemyCol.enabled = false;
		playerHealth.GetExp(25);
        Destroy(GetComponent<EnemyController>());
    } 
}
