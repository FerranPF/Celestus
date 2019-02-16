using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpell : MonoBehaviour
{
    private GameObject fireSpell;
    public float timeToEnd;
    public float timeToDamage;
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
            damage = true;
            contToDamage = 0.0f;
        }
        else
        {
            contToDamage += Time.deltaTime;
            damage = false;
        }
    }
    
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
    }

    void EndSpell()
    {
        Destroy(fireSpell);
    }

}
