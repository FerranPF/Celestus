using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class LightningSpell : MonoBehaviour
{
    public ParticleSystem[] PS;
    private BoxCollider coll;

    public bool m_Started;

    public float timeTravel;
    public float travelSpeed;
    private float contTime = 0.0f;
    public int damage;
    public int bossDamage;
    private bool active;
    private Vector3 speedVector;

    public AudioClip[] sounds;
    AudioSource audioSource;

    private void Start()
    {
        coll = this.GetComponent<BoxCollider>();
        coll.isTrigger = true;
        active = false;
        speedVector = new Vector3(0, 0, travelSpeed);
    }

    void SetAudio()
    {
        audioSource = GetComponent<AudioSource>();
        float i = Random.Range(0.0f, (float)sounds.Length);
        audioSource.clip = sounds[(int)i];
    }

    private void OnEnable()
    {
        SetAudio();
        audioSource.Play(0);
        for (int i = 0; i < PS.Length; i++)
        {
            PS[i].Play();
        }
    }

    private void Update()
    {
        contTime += Time.deltaTime;

        this.transform.Translate(speedVector*Time.deltaTime, Space.Self);

        //MyCollision();

        if(contTime >= timeTravel)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            EnemyController enemy = other.GetComponent<EnemyController>();
            enemy.GetDamage(damage);
            
        }
        else if (other.tag == "Father")
        {
            BossController boss = other.GetComponent<BossController>();
            boss.GetDamage(bossDamage);
        }
    }

    /*
    public void MyCollision()
    {
        Collider[] enemyColliders = Physics.OverlapBox(transform.position, transform.localScale, Quaternion.LookRotation(Vector3.forward, Vector3.up));
        int i = 0;
        
        while (i < enemyColliders.Length)
        {
            if (enemyColliders[i].tag == "Enemy")
            {
                EnemyController enemy = enemyColliders[i].GetComponent<EnemyController>();
                enemy.GetDamage(damage);
                
            }else if (enemyColliders[i].tag == "Father")
            {
                BossController boss = enemyColliders[i].GetComponent<BossController>();
                boss.GetDamage(bossDamage);
            }
            i++;
        }
    }
    */

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (m_Started)
            Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
