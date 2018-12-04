using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {


    public float lookRadius = 10.0f;
    Transform target;
    NavMeshAgent agent;

    public int enemyHealth = 100;
    private float attackTime;
    private float waitingTime;
	private PlayerHealth playerHealth;
	
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = PlayerManager.instance.player.transform;
		playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        attackTime = 2.0f;
        waitingTime = 0.0f;
    }

    private void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if(distance <= lookRadius){
            agent.SetDestination(target.position);
            if(distance <= agent.stoppingDistance){
                FaceTarget();
                Attack();
            }else{
            waitingTime = 0;
            }
        }
    }

    void Attack(){

        if(attackTime <= waitingTime){
            playerHealth.TakeDamage(10);
            Debug.Log("Attacking");
            waitingTime = 0;
        }else{
            waitingTime += Time.deltaTime;
            Debug.Log("Thinking");
        }
    }
    
    void FaceTarget(){
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = lookRotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
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
