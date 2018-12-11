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
    private bool expl;

    void Start () {
        audio = GetComponent<AudioSource>();
        audio.clip = castAudio;
        audio.Play(0);
        timeToDestroy = 0.0f;
        player = GameObject.FindGameObjectWithTag("Player");
        transform.Rotate(new Vector3(0, player.transform.localEulerAngles.y, 0));
        destTime = 5.0f;
        dead = false;
        expl = false;
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
            StartCoroutine(DestroySpell());
        }

        if(expl){
            Explosion();
            expl = false;
            dead = true;
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag != "Player")
        {
            if (other.gameObject.tag == "Enemy")
            {
                Debug.Log("Enemy");
                EnemyController enemyHealth;
                enemyHealth = other.GetComponent<EnemyController>();
                enemyHealth.GetDamage(damage);
                expl = true;
            }
            if(other.gameObject.tag == "Environment")
            {
                Debug.Log("Environment");
                dead = true;
            }
        }
    }

    void Explosion(){
        /*
        audio.clip = explAudio;
        Debug.Log("Explosion");
        audio.Play(0);
        BoxCollider col;
        col = GetComponent<BoxCollider>();
        col.enabled = false;
         */
        Destroy(gameObject);
    }
    
    IEnumerator DestroySpell(){
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
}
