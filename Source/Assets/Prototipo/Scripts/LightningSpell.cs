using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningSpell : MonoBehaviour
{
    public ParticleSystem[] PS;
    private BoxCollider coll;

    public float timeTravel;
    public float travelSpeed;
    private float contTime = 0.0f;
    public int damage;
    private bool active;
    private Vector3 speedVector;

    private void Start()
    {
        coll = this.GetComponent<BoxCollider>();
        coll.isTrigger = true;
        active = false;
        speedVector = new Vector3(0, 0, travelSpeed);
    }

    private void OnEnable()
    {
        for (int i = 0; i < PS.Length; i++)
        {
            PS[i].Play();
        }
    }

    private void Update()
    {
        contTime += Time.deltaTime;

        this.transform.Translate(speedVector*Time.deltaTime, Space.Self);

        if(contTime >= timeTravel)
        {
            Destroy(this.gameObject);
        }
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
}
