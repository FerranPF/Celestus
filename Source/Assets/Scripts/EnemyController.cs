using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    public GameObject damageText;

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

    public EnemySword swordDamage;

    public bool frozen = false;
    private float timeFreeze;
    private float freezeCont = 0.0f;
    public float enemySpeed;
    private bool dead = false;

    public float delayAttack = 0.2f;
    public int healthRecovery = 20;
    public float manaRecovery = 10;

    private void Start()
    {
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
        if (!dead)
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
                if (agent.velocity == Vector3.zero)
                {
                    animator.SetBool("walking", false);
                }
            }

            if (frozen)
            {
                freezeCont += Time.deltaTime;
                Debug.Log(timeFreeze);
                if (freezeCont >= timeFreeze)
                {
                    freezeCont = 0.0f;
                    if (!dead)
                    {
                        canMove = true;
                    }
                    frozen = false;
                }
            }
        }
        else
        {
            DisableEnemy();
        }
    }

    private void DisableEnemy()
    {
        canAttack = false;
        canMove = false;
        agent.enabled = false;
    }

    public void Freeze(float timeFrozen)
    {
        frozen = true;
        timeFreeze = timeFrozen;
        canMove = false;
        agent.SetDestination(this.transform.position);
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

        if (damageText && enemyHealth > 0)
        {
            ShowFloatingText();
        }

        Debug.Log("Enemy health: " + enemyHealth);

        if (enemyHealth <= 0)
        {
            StartCoroutine(Death());
        }
    }

    void ShowFloatingText()
    {
        var go = Instantiate(damageText, transform.position, Quaternion.identity, transform);
        go.GetComponent<TextMesh>().text = enemyHealth.ToString();
    }

    IEnumerator Death()
    {
        coll.enabled = false;
        playerHealth.GetExp(25);
        frozen = false;
        canMove = false;
        dead = true;
        animator.SetBool("death", true);
        animator.SetBool("attacking", false);
        animator.SetBool("walking", false);

        playerHealth.HealthRecovery(healthRecovery);
        playerHealth.ManaRecovery(manaRecovery);
        yield return new WaitForSeconds(5.0f);
        Destroy(gameObject);
    }

    public void AttackOverlap()
    {
        swordDamage.MyCollision();
    }

    void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
