using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : MonoBehaviour
{

    public Transform target;
    public Transform pointOfAttack;
    public GameObject damageText;
    private GameManager manager;

    public float range = 15f;
    public float damage;

    public bool showGizmo;

    private string playerTag = "Player";
    public bool canAttack;
    public bool attack;
    private bool findPlayer;

    private float contColor;

    private float cont = 0f;
    public float timeToAttack = 2f;
    public float timeAttacking = 1f;

    public float turretHealth = 20f;

    public LineRenderer ray;
    public Color colorIdle;
    public Color colorAttack;

    public GameObject ps;
    public bool defeat = false;

    private CapsuleCollider coll;
    private Animator anim;

    void Start()
    {
        canAttack = false;
        findPlayer = true;
        target = GameObject.FindGameObjectWithTag(playerTag).GetComponent<Transform>();
        ray.SetPosition(0, pointOfAttack.position);
        ray.enabled = false;
        coll = GetComponent<CapsuleCollider>();
        anim = GetComponent<Animator>();
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }



    void Update()
    {
        if (!defeat)
        {
            if (findPlayer)
            {
                Invoke("UpdateTarget", 0.5f);
            }

            if (canAttack)
            {
                Debug.DrawRay(pointOfAttack.position, target.position - pointOfAttack.position);
                ray.enabled = true;
                ray.SetPosition(1, target.position);
                ray.material.color = Color.Lerp(colorIdle, colorAttack, cont / timeToAttack);

                cont += Time.deltaTime;

                if (cont >= timeToAttack)
                {
                    attack = true;
                }
            }

            if (attack)
            {
                StartCoroutine(Attack());
            }
        }
    }
     
    void UpdateTarget()
    {
        float distanceToPlayer = Vector3.Distance(pointOfAttack.position, target.transform.position);

        if (distanceToPlayer <= range)
        {
            canAttack = true;
        }
        else
        {
            canAttack = false;
            cont = 0.0f;
            ray.enabled = false;
        }
    }

    IEnumerator Attack()
    {
        canAttack = false;
        cont = 0.0f;
        findPlayer = false;
        attack = false;
        target.gameObject.GetComponent<PlayerStats>().TakeDamage(damage);
        Instantiate(ps, pointOfAttack.transform);

        yield return new WaitForSeconds(timeAttacking);

        ray.material.color = colorIdle;
        findPlayer = true;
    }

    public void TakeDamage(float damageTaken)
    {
        turretHealth -= damageTaken;
        if(turretHealth <= 0)
        {
            DisableTurret();
        }
        ShowFloatingText();
    }

    void DisableTurret()
    {
        defeat = true;
        turretHealth = 0;
        ray.enabled = false;
        coll.enabled = false;
        anim.SetBool("death", true);
        manager.KillTurret();
    }

    void ShowFloatingText()
    {
        var go = Instantiate(damageText, transform.position, Quaternion.identity, transform);
        go.GetComponent<TextMesh>().text = turretHealth.ToString();
    }

    private void OnDrawGizmos()
    {
        if (showGizmo)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(pointOfAttack.position, range);
        }
    }
}
