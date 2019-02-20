using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningSpell : MonoBehaviour
{
    public ParticleSystem PS;
    private BoxCollider coll;
    public float pulseTime;
    public int timesPulse;
    private float contTime = 0.0f;
    public int damage;


    private void Start()
    {
        coll = this.GetComponent<BoxCollider>();
        coll.isTrigger = true;
        coll.enabled = false;
        PS = GetComponentInChildren<ParticleSystem>();
    }

    private void Update()
    {
        if (contTime < pulseTime)
        {
            contTime += Time.deltaTime;
            StartCoroutine(Pulse());
        }
    }

    IEnumerator Pulse()
    {
        coll.enabled = true;
        yield return new WaitForSeconds(pulseTime/(timesPulse+0.1f));
        coll.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Enemy")
        {
            EnemyController enemy;
            enemy = other.gameObject.GetComponent<EnemyController>();
            enemy.GetDamage(damage);
        }
    }

    public void ActivateSpell1()
    {
        contTime = 0;
        PS.Play();
    }
}
