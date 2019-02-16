using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningSpell : MonoBehaviour
{
    public ParticleSystem PS;

    private void Start()
    {
        PS = GetComponentInChildren<ParticleSystem>();
    }

    private void OnParticleTrigger()
    {
        
    }

    void OnParticleCollision(GameObject coll)
    {
        Debug.Log(coll.tag);
        if(coll.tag == "Enemy")
        {
            EnemyController enemy;
            enemy = coll.GetComponent<EnemyController>();
            enemy.GetDamage(10);
        }
    }
}
