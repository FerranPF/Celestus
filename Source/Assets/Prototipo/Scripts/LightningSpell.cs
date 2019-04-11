using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningSpell : MonoBehaviour
{
    public ParticleSystem[] PS;
    private BoxCollider coll;

    public bool m_Started;

    public float timeTravel;
    public float travelSpeed;
    private float contTime = 0.0f;
    public int damage;
    private bool active;
    private Vector3 speedVector;
    private string lastEnemy = " ";

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

        MyCollision();

        if(contTime >= timeTravel)
        {
            Destroy(this.gameObject);
        }
    }

    public void MyCollision()
    {
        Collider[] enemyColliders = Physics.OverlapBox(transform.position, transform.localScale, Quaternion.LookRotation(Vector3.forward, Vector3.up));
        int i = 0;

        //Check when there is a new collider coming into contact with the box
        while (i < enemyColliders.Length)
        {
            //Damage every enemy in the collider
            //Debug.Log("Hit : " + enemyColliders[i].name + i);
            if (enemyColliders[i].tag == "Enemy")
            {
                Debug.Log(enemyColliders[i].gameObject.name);

                if(enemyColliders[i].gameObject.name != lastEnemy)
                {
                    EnemyController enemy = enemyColliders[i].GetComponent<EnemyController>();
                    lastEnemy = enemyColliders[i].gameObject.name;
                    enemy.GetDamage(damage);
                }
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
