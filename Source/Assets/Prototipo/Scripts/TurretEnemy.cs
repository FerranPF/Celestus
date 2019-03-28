using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : MonoBehaviour
{

    public Transform target;
    public Transform pointOfAttack;
    public GameObject damageText;

    public float range = 15f;
    public float damage;

    public bool showGizmo;

    private string playerTag = "Player";
    public bool canAttack;
    public bool attack;
    private bool findPlayer;

    private float cont = 0f;
    public float timeToAttack = 2f;
    public float timeAttacking = 1f;

    public float turretHealth = 20f;

    public LineRenderer ray;

    // Start is called before the first frame update
    void Start()
    {
        canAttack = false;
        findPlayer = true;
        target = GameObject.FindGameObjectWithTag(playerTag).GetComponent<Transform>();
        ray.SetPosition(0, pointOfAttack.position);
        ray.enabled = false;
    }


    // Update is called once per frame
    void Update()
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
            cont += Time.deltaTime;
            if(cont >= timeToAttack)
            {
                attack = true;
            }
        }


        if (attack)
        {
            StartCoroutine(Attack());
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

        yield return new WaitForSeconds(timeAttacking);
        
        findPlayer = true;

    }

    public void TakeDamage(float damageTaken)
    {
        if(turretHealth - damageTaken <= 0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            turretHealth -= damageTaken;
            ShowFloatingText();
        }
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
