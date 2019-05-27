using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpell : MonoBehaviour
{
    public GameObject Spell;
    public bool m_Started;
    public float timeSpawn;
    private float cont;
    public float timeFreeze;
    public int damage;

    public AudioClip[] sounds;
    AudioSource audioSource;

    private void Update()
    {
        MyCollision();
        cont += Time.deltaTime;

        if(cont >= timeSpawn)
        {
            Destroy(Spell);
        }
    }

    private void OnEnable()
    {
        SetAudio();
        audioSource.Play(0);
    }

    void SetAudio()
    {
        audioSource = GetComponent<AudioSource>();
        float i = Random.Range(0.0f, (float)sounds.Length);
        audioSource.clip = sounds[(int)i];
    }

    public void MyCollision()
    {
        Collider[] enemyColliders = Physics.OverlapBox(transform.position, transform.localScale, Quaternion.LookRotation(Vector3.forward, Vector3.up));
        int i = 0;

        while (i < enemyColliders.Length)
        {
            Debug.Log("Hit : " + enemyColliders[i].name + i);
            if (enemyColliders[i].tag == "Enemy")
            {
                EnemyController enemy = enemyColliders[i].GetComponent<EnemyController>();
                enemy.Freeze(timeFreeze);
            }else if(enemyColliders[i].tag == "Father")
            {
                BossController boss = enemyColliders[i].GetComponent<BossController>();
                boss.Freeze(timeFreeze);
            }

            i++;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (m_Started)
            Gizmos.DrawWireCube(transform.position, transform.localScale);
    }

}
