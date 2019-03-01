using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDamage : MonoBehaviour
{
    public float expandSpeed;
    public float damage;

    private void Update()
    {
        this.transform.localScale += new Vector3(expandSpeed, expandSpeed, 0)*Time.deltaTime;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerStats>().TakeDamage(damage);
        }
    }
}
