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
    public GameObject fire;
    private AudioSource audio;
    public AudioClip castAudio;
    public AudioClip explAudio;
    private bool dead;

    private float destTime;

    void Start () {
        audio = GetComponent<AudioSource>();
        audio.clip = castAudio;
        audio.Play(0);
        timeToDestroy = 0.0f;
        player = GameObject.FindGameObjectWithTag("Player");
        transform.Rotate(new Vector3(0, player.transform.localEulerAngles.y, 0));
        destTime = 5.0f;
        dead = false;
    }
	


	void Update () {
        if(!dead){
            timeToDestroy += Time.deltaTime;
            transform.Translate(new Vector3(0, 0, speedSpell*Time.deltaTime), Space.Self);
        }

        if(timeToDestroy >= lifeTime){
            dead = true;
        }

        if(dead){
            StartCoroutine(Explosion());
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
                dead = false;
            }
            if(other.gameObject.tag == "Environment")
            {
                dead = true;
            }
        }
    }

    IEnumerator Explosion(){
        audio.clip = explAudio;
        audio.Play(0);
        BoxCollider col;
        col = GetComponent<BoxCollider>();
        col.enabled = false;

        yield return new WaitForSeconds(2.0f);

        Destroy(gameObject);
    }
}
