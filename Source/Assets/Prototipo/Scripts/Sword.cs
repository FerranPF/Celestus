using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour {

    public int damage = 10;
    public bool sangrado;

    private void Start()
    {
        sangrado = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            EnemyController enemy = other.GetComponent<EnemyController>();
            enemy.GetDamage(damage);
            if (sangrado)
            {
                Debug.Log("sangrado");
                enemy.sangrado = true;
            }
        }

        if (other.gameObject.tag == "Boss")
        {
            BossController boss = other.GetComponent<BossController>();
            boss.GetDamage(damage);
            if (sangrado)
            {
                boss.sangrado = true;
            }
        }
    }
}
