using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            EnemyController enemy = other.GetComponent<EnemyController>();
            enemy.GetDamage(10);
        }

        if (other.gameObject.tag == "Boss")
        {
            BossController boss = other.GetComponent<BossController>();
            boss.GetDamage(10);
        }
    }
}
