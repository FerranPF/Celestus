using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour {

    float timeToDestroy;
    public float lifeTime;
    public float speedSpell;
    public int damage;
    GameObject player;
    public GameObject fire;
    private AudioSource audioSource;
    public AudioClip castAudio;
    public AudioClip explAudio;
    private bool expl;

    void Start () {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = castAudio;
        audioSource.Play(0);
        timeToDestroy = 0.0f;
        player = GameObject.FindGameObjectWithTag("Player");
        transform.Rotate(new Vector3(0, player.transform.localEulerAngles.y, 0));
        expl = false;
    }
	


	void Update () {
        if(!expl){
            timeToDestroy += Time.deltaTime;
            transform.Translate(new Vector3(0, 0, speedSpell*Time.deltaTime), Space.Self);
        }

        if(timeToDestroy >= lifeTime){
            expl = true;
        }

        if(expl){
            Explosion();
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

            if(other.gameObject.tag == "Boss")
            {
                Debug.Log("Boss");
                BossController bossHealth;
                bossHealth = other.GetComponent<BossController>();
                bossHealth.GetDamage(damage);
                expl = true;
            }

            if(other.gameObject.tag == "Environment")
            {
                Debug.Log("Environment");
                expl = true;
            }
        }
    }

    void Explosion(){
        /*
        audioSource.clip = explAudio;
        Debug.Log("Explosion");
        audioSource.Play(0);
        BoxCollider col;
        col = GetComponent<BoxCollider>();
        col.enabled = false;
         */
        if (audioSource.isPlaying == false)
        {
            Destroy(gameObject);
        }
    }
}
