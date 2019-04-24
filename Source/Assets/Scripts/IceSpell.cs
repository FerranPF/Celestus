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

        //Check when there is a new collider coming into contact with the box
        while (i < enemyColliders.Length)
        {
            //Damage every enemy in the collider
            Debug.Log("Hit : " + enemyColliders[i].name + i);
            if (enemyColliders[i].tag == "Enemy")
            {
                EnemyController enemy = enemyColliders[i].GetComponent<EnemyController>();
                enemy.Freeze(timeFreeze);
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
