using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {


    public float lookRadius = 10.0f;
    Transform target;
    NavMeshAgent agent;

    public int enemyHealth = 100;
    private CapsuleCollider coll;

    private Animator animator;
    public AnimationClip attackAnim;
    private bool canMove;
    private PlayerStats playerHealth;
    private bool canAttack;
    private float attackTime;

    public bool sangrado = false;
    private int contSangrado;
    private int sangradoTime;
    public int sangradoDamage;
    private float secCont = 0;

    public bool frozen = false;
    private float timeFreeze;
    private float freezeCont = 0.0f;
    public float enemySpeed;

    private void Start()
    {
        sangradoTime = 5;
        sangradoDamage = 2;
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        target = PlayerManager.instance.player.transform;
        canMove = true;
        canAttack = true;
        attackTime = (attackAnim.length) * 1.2f;
        coll = this.GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        if (canMove)
        {
            float distance = Vector3.Distance(target.position, transform.position);

            if (distance <= lookRadius)
            {
                agent.SetDestination(target.position);
                agent.speed = enemySpeed;
                animator.SetBool("walking", true);

                if (distance <= agent.stoppingDistance)
                {
                    if (canAttack)
                    {
                        StartCoroutine(Attack());
                    }
                }
            }
        }

        if (sangrado)
        {
            Debug.Log("Sangrando");
            secCont += Time.deltaTime;
            if(secCont >= 1.0f)
            {
                GetDamage(sangradoDamage);
                contSangrado++;
                secCont = 0.0f;
            }

            if(contSangrado == sangradoTime)
            {
                sangrado = false;
            }
        }

        if (frozen)
        {
            freezeCont += Time.deltaTime;
            Debug.Log(timeFreeze);
            if (freezeCont >= timeFreeze)
            {
                freezeCont = 0.0f;
                canMove = true;
                frozen = false;
                agent.Resume();
            }
        }
    }

    public void Freeze(float timeFrozen)
    {
        frozen = true;
        timeFreeze = timeFrozen;
        canMove = false;
        agent.Stop();
        animator.SetBool("walking", false);
        animator.SetBool("attacking", false);
    }

    IEnumerator Attack()
    {
        FaceTarget();
        canMove = false;
        animator.SetBool("attacking", true);
        canAttack = false;
        animator.SetBool("walking", false);
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
        if (enemyHealth <= 0)
        {
            animator.SetBool("death", true);
            StartCoroutine(Death());
        }
    }

    IEnumerator Death()
    {
		playerHealth.GetExp(25);
        canMove = false;
        coll.enabled = false;
        agent.Stop();
        yield return new WaitForSeconds(5.0f);
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
