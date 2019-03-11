using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpell : MonoBehaviour
{
    private BoxCollider coll;
    public float timeFrozen;
    float cont = 0.0f;
    bool activated = false;
    public ParticleSystem[] PS;

    private void Start()
    {
        coll = GetComponent<BoxCollider>();
        coll.enabled = false;
    }
    
    public void ActivateSpell()
    {
        for(int i = 0; i < PS.Length; i++){
            PS[i].Play();
        }
        coll.enabled = true;
        activated = true;
    }

    private void Update()
    {
        if (activated)
        {
            cont += Time.deltaTime;
            if (cont >= 0.5)
            {
                cont = 0.0f;
                coll.enabled = false;
                activated = false;
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            EnemyController enemy;
            enemy = other.gameObject.GetComponent<EnemyController>();

            enemy.Freeze(timeFrozen);
        }
    }

}
