using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class FireBall : MonoBehaviour {

    float timeToDestroy;
    public float lifeTime;
    public float speedSpell;
    public int damage;
    GameObject player;
    private AudioSource audio;
    public AudioClip[] audioClip;

    private float destTime;

    void Start () {
        audio = GetComponent<AudioSource>();
        audio.clip = audioClip[0];
        audio.Play(0);
        timeToDestroy = 0.0f;
        player = GameObject.FindGameObjectWithTag("Player");
        transform.Rotate(new Vector3(0, player.transform.localEulerAngles.y, 0));
        destTime = 5.0f;
    }
	


	void Update () {
        timeToDestroy += Time.deltaTime;
        transform.Translate(new Vector3(0, 0, speedSpell*Time.deltaTime), Space.Self);

        if (timeToDestroy >= lifeTime)
        {
            DestroySpell();
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag != "Player")
        {
            if (other.gameObject.tag == "Enemy")
            {
                EnemyController enemyHealth;
                enemyHealth = other.GetComponent<EnemyController>();
                enemyHealth.GetDamage(damage);
                DestroySpell();
            }
            if(other.gameObject.tag == "Environment")
            {
                DestroySpell();
            }
        }
        
    }

    void DestroySpell()
    {
        audio.clip = audioClip[1];
        audio.Play(0);

        BoxCollider col;
        col = GetComponent<BoxCollider>();
        col.enabled = false;
        
        ParticleSystem particle;
        particle = GetComponentInChildren<ParticleSystem>();
        particle.Stop(true);
    }

}
