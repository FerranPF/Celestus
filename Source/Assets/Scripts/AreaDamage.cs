using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDamage : MonoBehaviour
{
    public float expandSpeed;
    public float damage;
    private PlayerController player;
    public GameObject particles;
    private float cont;
    public float timeToDestroy = 5.0f;

    private void Update()
    {
        this.transform.localScale += new Vector3(expandSpeed, expandSpeed, 0)*Time.deltaTime;

        cont += Time.deltaTime;
        if(cont >= timeToDestroy)
        {
            Destroy(this.gameObject);
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            player = other.gameObject.GetComponent<PlayerController>();

            if (!player.dashing)
            {
                other.gameObject.GetComponent<PlayerStats>().TakeDamage(damage);
            }
        }
    }
}
