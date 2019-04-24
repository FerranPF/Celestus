using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDamage : MonoBehaviour
{
    public bool m_Started;

    public void MyCollision()
    {
        Collider[] enemyColliders = Physics.OverlapBox(transform.position, transform.localScale, Quaternion.LookRotation(Vector3.forward, Vector3.up));
        int i = 0;

        //Check when there is a new collider coming into contact with the box
        while (i < enemyColliders.Length)
        {
            //Damage every enemy in the collider
            Debug.Log("Hit : " + enemyColliders[i].name + i);
            if(enemyColliders[i].tag == "Enemy")
            {
                EnemyController enemy = enemyColliders[i].GetComponent<EnemyController>();
                enemy.GetDamage(Random.Range(4, 6));
            }
            if(enemyColliders[i].tag == "Turret")
            {
                TurretEnemy turret = enemyColliders[i].GetComponent<TurretEnemy>();
                turret.TakeDamage(Random.Range(3, 5));
            }
            if(enemyColliders[i].tag == "Father")
            {
                BossController father = enemyColliders[i].GetComponent<BossController>();
                father.GetDamage(Random.Range(6, 8));
            }
            //Increase the number of Colliders in the array
            i++;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Check that it is being run in Play Mode, so it doesn't try to draw this in Editor mode
        if (m_Started)
            //Draw a cube where the OverlapBox is (positioned where your GameObject is as well as a size)
            Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
