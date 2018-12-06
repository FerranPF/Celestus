using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour {

    float timeToDestroy;
    public float lifeTime;
    public float speedSpell;
    public int damage;
    GameObject player;

    void Start () {
        timeToDestroy = 0.0f;
        player = GameObject.FindGameObjectWithTag("Player");
        transform.Rotate(new Vector3(0, player.transform.localEulerAngles.y, 0));
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
        Destroy(gameObject);
    }

}
