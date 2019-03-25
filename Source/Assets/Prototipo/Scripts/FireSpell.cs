using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpell : MonoBehaviour
{
    private GameObject fireSpell;
    public float timeToEnd;
    public float timeToDamage;

    public float damageArea;

    private float contToDamage;
    private bool damage = false;
    public int enemyDamage;

    private void Start()
    {
        fireSpell = this.gameObject;
    }

    private void Update()
    {
        timeToEnd -= Time.deltaTime;

        if(timeToEnd <= 0.0f)
        {
            EndSpell();
        }

        if (contToDamage >= timeToDamage)
        {
            FireDamage(this.transform.position, damageArea);
            contToDamage = 0.0f;
        }
        else
        {
            contToDamage += Time.deltaTime;
            damage = false;
        }
    }

    /*
    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Enemy")
        {
            EnemyController enemy;
            enemy = other.gameObject.GetComponent<EnemyController>();
            if (damage)
            {
                enemy.GetDamage(enemyDamage);
            }
        }
    }*/

    void EndSpell()
    {
        Destroy(fireSpell);
    }

    void FireDamage(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if(hitColliders[i].gameObject.tag == "Enemy")
            {
                EnemyController enemy;
                enemy = hitColliders[i].gameObject.GetComponent<EnemyController>();
                enemy.GetDamage(enemyDamage);
            }
            i++;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, damageArea);
    }

}
