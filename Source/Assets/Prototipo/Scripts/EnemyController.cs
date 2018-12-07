﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {


    public float lookRadius = 10.0f;
    Transform target;
    NavMeshAgent agent;

    public int enemyHealth = 100;

    private Animator animator;
    public AnimationClip attackAnim;
    private bool canMove;
    private PlayerHealth playerHealth;
    private bool canAttack;
    private float attackTime;

    private void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        target = PlayerManager.instance.player.transform;
        canMove = true;
        canAttack = true;
        attackTime = (attackAnim.length) * 0.9f;
    }

    private void Update()
    {
        if (canMove)
        {
            float distance = Vector3.Distance(target.position, transform.position);

            if (distance <= lookRadius)
            {
                agent.SetDestination(target.position);
                if (distance <= agent.stoppingDistance)
                {
                    if (canAttack)
                    {
                        StartCoroutine(Attack());
                    }
                }
            }
        }
        
    }

    IEnumerator Attack()
    {
        FaceTarget();
        canMove = false;
        animator.SetBool("attacking", true);
        canAttack = false;
        yield return new WaitForSeconds(attackTime);
        canMove = true;
        animator.SetBool("attacking", false);
        canAttack = true;

    }
    
    void FaceTarget(){
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = lookRotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 2f);
    }

    public void GetDamage(int damage)
    {
        enemyHealth -= damage;
        Debug.Log("Enemy health: " + enemyHealth);
        if (enemyHealth == 0)
        {
            Death();
        }
    }

    protected void Death()
    {
		playerHealth.GetExp(25);
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
