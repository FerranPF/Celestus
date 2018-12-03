using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {


    public float lookRadius = 10.0f;
    Transform target;
    NavMeshAgent agent;

    public int enemyHealth = 100;
    private CapsuleCollider enemyCol;
    private Renderer enemyRen;
	
	private GameObject player;
	private PlayerHealth playerHealth;
	
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = PlayerManager.instance.player.transform;
		player = GameObject.FindGameObjectWithTag("Player");
		playerHealth = player.GetComponent<PlayerHealth>();
		
        enemyRen = GetComponentInChildren<Renderer>();
        enemyCol = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if(distance <= lookRadius){
            agent.SetDestination(target.position);
            if(distance <= agent.stoppingDistance){
                FaceTarget();
            }
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
        enemyRen.enabled = false;
        enemyCol.enabled = false;
		playerHealth.GetExp(25);
        Destroy(GetComponent<EnemyController>());
    }

    void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
