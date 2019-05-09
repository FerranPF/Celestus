﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{
    public int bossHealth = 100;

    private Animator animator;
    public AnimationClip attack;
    public GameObject areaAttack;

    public bool defeat = false;
    public bool canAttack;
    private float cont = 0.0f;
    public float timeOfAttack;
    public float timeToAttack;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        animator.SetBool("attack", false);
        timeOfAttack = attack.length;
        canAttack = false;
    }

    private void Update()
    {
        if (!defeat)
        {
            if (canAttack)
            {
                cont += Time.deltaTime;
                if (cont >= timeToAttack)
                {
                    StartCoroutine(Attack());
                }
            }
        }
    }

    IEnumerator Attack()
    {
        canAttack = false;
        cont = 0.0f;
        animator.SetBool("attack", true);

        yield return new WaitForSeconds(timeOfAttack/3);

        Instantiate(areaAttack, this.transform.position, Quaternion.Euler(270, 0, 0), this.transform);
        animator.SetBool("attack", false);
        canAttack = true;
    }

    public void GetDamage(int damage)
    {
        bossHealth -= damage;
        Debug.Log("Boss health: " + bossHealth);
        if (bossHealth <= 0)
        {
            WinGame();
        }
    }

    void WinGame()
    {
        MyGameManager manager;
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<MyGameManager>();
        manager.Win();
        Destroy(gameObject);
    }
}