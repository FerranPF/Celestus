using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	
	public Animator anim;
	public AnimationClip attackAnim;
	private float animTime;
	private float attackTime;
	public bool canMove;
    public BoxCollider sword;
    PlayerHealth health;
    public bool canAttack;
    public bool canSpell;

    private float spellCount;
    public float dashForce;
    public bool godMode;

    public AnimationClip spellAnim;

	[SerializeField]
	float movementSpeed = 4.0f;

    private void Awake()
    {
        health = GetComponent<PlayerHealth>();
    }

    void Start(){
		anim = GetComponentInChildren<Animator>();
        attackTime = attackAnim.length;
		attackTime *= 0.9f;
		canMove = true;
        canSpell = true;
        canAttack = true;
        sword.enabled = false;
        spellCount = 1f;
	}

	void Update(){

        if(godMode){
            GodMode();
        }

        if(Input.GetKeyDown(KeyCode.Space)){
            Debug.Log("Dash");
        }

        if (canMove)
		{

            if (Input.GetKeyDown(KeyCode.Alpha1) && canSpell)
            {
                if (spellCount >= 1f && health.currentMana > 0f)
                {
                    health.UseMana(10f);
                    spellCount = 0.0f;
                    health.SpellCD.fillAmount = 0.0f;
                    StartCoroutine(CastSpell());
                }
                else
                {
                    Debug.Log("CD time");
                }
            }
            
            if (Input.GetAxisRaw("Fire1")>0 && canAttack)
            {
                StartCoroutine(Attack());
            }

            ControlPlayer();            
		}

        do{
            spellCount += Time.deltaTime;
            health.SpellCD.fillAmount = spellCount;
        }while(spellCount <= 1.0f);
        
    }

    IEnumerator Attack(){

        anim.SetBool("attack", true);
        RotatePlayer();
        canMove = false;
        canSpell = false;
        canAttack = false;
        sword.enabled = true;
        
        yield return new WaitForSeconds(attackTime);

        anim.SetBool("attack", false);
        canSpell = true;
		canMove = true;
        canAttack = true;
        sword.enabled = false;
    }

	void ControlPlayer(){
		float moveHorizonat = Input.GetAxisRaw("Horizontal");
		float moveVertical = Input.GetAxisRaw("Vertical");
		
		Vector3 movement = new Vector3(moveHorizonat, 0.0f, moveVertical);
		movement.Normalize();
		
		if(movement != Vector3.zero) transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement.normalized), 0.2f);
		transform.Translate (movement*movementSpeed*Time.deltaTime, Space.World);

		if(movement != Vector3.zero){
			anim.SetBool("moving", true);
		}else{
			anim.SetBool("moving", false);
		}
	}

	void RotatePlayer()
	{
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Plane groundPlane = new Plane(Vector3.up, new Vector3(0, 1, 0));
		float rayLength;
        Vector3 pointToLook;
        
		if(groundPlane.Raycast(ray, out rayLength))
		{
			pointToLook = ray.GetPoint(rayLength);
            //Debug.Log(pointToLook);
            transform.LookAt(pointToLook);
            //Debug.Log(transform.rotation);
		}
	}

    IEnumerator CastSpell()
    {
        RotatePlayer();
        anim.SetBool("spell", true);
        canMove = false;
        canAttack = false;
        canSpell = false;

        yield return new WaitForSeconds(spellAnim.length);

        anim.SetBool("spell", false);
        canMove = true;
        canSpell = true;
        canAttack = true;
    }


    void GodMode()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            health.TakeDamage(10f);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            health.HealthRecovery(10f);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            health.UseMana(10f);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            health.ManaRecovery(10f);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            health.GetExp(10f);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            health.GetExp(50f);
        }
    }
}
