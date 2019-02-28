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

    public EnemyWeapon weapon;

    public bool sangrado = false;
    private int contSangrado;
    private int sangradoTime;
    public int sangradoDamage;
    private float secCont = 0;

    public bool frozen = false;
    private float timeFreeze;
    private float freezeCont = 0.0f;
    public float enemySpeed;
    private bool dead = false;

    private float cont;
    public float delayAttack = 0.2f;

    bool chasing = false;

    private enum EnemyPhase
    {
        Idle,
        Chase,
        Walk,
        Attack,
        Frozen,
        Death
    }

    private EnemyPhase enemyPhase = EnemyPhase.Idle;
    private void Start()
    {
        weapon = GetComponentInChildren<EnemyWeapon>();
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
        switch (enemyPhase)
        {
            case EnemyPhase.Idle:
                EnemyIdle();
                break;

            case EnemyPhase.Chase:
                EnemyChase();
                break;

            case EnemyPhase.Walk:
                EnemyWalk();
                break;

            case EnemyPhase.Attack:
                EnemyAttack();
                break;

            case EnemyPhase.Frozen:
                EnemyFrozen();
                break;

            case EnemyPhase.Death:
                EnemyDeath();
                break;
        }
    }

    private void EnemyIdle()
    {
        animator.SetBool("walking", false);
        animator.SetBool("attacking", false);
        animator.SetBool("death", false);

        DetectPlayer();
    }

    private void EnemyChase()
    {
        animator.SetBool("walking", true);
        animator.SetBool("attacking", false);
        animator.SetBool("death", false);
        agent.SetDestination(target.position);
        agent.speed = enemySpeed;

        DetectPlayer();
    }

    private void EnemyWalk()
    {
        animator.SetBool("death", false);
        animator.SetBool("attacking", false);
        animator.SetBool("walking", true);
    }

    private void EnemyAttack()
    {
        animator.SetBool("death", false);
        animator.SetBool("attacking", true);
        animator.SetBool("walking", false);
        canAttack = false;

        weapon.coll.enabled = true;
        StartCoroutine(Attack());
    }

    private void EnemyFrozen()
    {
        animator.SetBool("death", false);
        animator.SetBool("attacking", false);
        animator.SetBool("walking", false);

        agent.SetDestination(transform.position);
        freezeCont += Time.deltaTime;
        if (freezeCont >= timeFreeze)
        {
            freezeCont = 0.0f;
            DetectPlayer();
        }
    }

    private void EnemyDeath()
    {
        animator.SetBool("death", true);
        animator.SetBool("attacking", false);
        animator.SetBool("walking", false);
        agent.SetDestination(this.transform.position);
        weapon.coll.enabled = false;

        StartCoroutine(Death());
    }

    private void DetectPlayer()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            if (chasing)
            {

            }
            enemyPhase = EnemyPhase.Chase;

            if (distance <= agent.stoppingDistance)
            {
                if (canAttack)
                {
                    enemyPhase = EnemyPhase.Attack;
                }
            }
        }
    }

    public void Freeze(float timeFrozen)
    {
        timeFreeze = timeFrozen;
        enemyPhase = EnemyPhase.Frozen;
    }

    IEnumerator Attack()
    {
        FaceTarget();
        weapon.coll.enabled = true;
        yield return new WaitForSeconds(attackTime);
        canAttack = true;
        DetectPlayer();
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
            enemyPhase = EnemyPhase.Death;
            playerHealth.GetExp(25);
        }
    }

    IEnumerator Death()
    {
        coll.enabled = false;
        yield return new WaitForSeconds(5.0f);
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
