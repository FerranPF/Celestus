using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossManager : MonoBehaviour
{

    private BoxCollider eventColl;
    public BossController boss;
    public GameObject hpBar;

    private void Start()
    {
        boss = GetComponentInParent<BossController>();
        eventColl = GetComponent<BoxCollider>();
        hpBar.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            boss.canAttack = true;
            hpBar.SetActive(true);
        }
    }

}
