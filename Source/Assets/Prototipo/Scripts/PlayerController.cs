using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	
	public Animator anim;
	public AnimationClip attackAnim;
	private float animTime;
	private float attackTime;
	public bool canMove;
    public CapsuleCollider sword;
    PlayerHealth health;

    private float spellCount;

	[SerializeField]
	float movementSpeed = 4.0f;

    private void Awake()
    {
        health = GetComponent<PlayerHealth>();
    }

    void Start(){
		anim = GetComponent<Animator>();
		attackTime = attackAnim.length;
		canMove = true;
        sword.enabled = false;
        spellCount = 1f;
	}

	void Update(){

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
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (spellCount >= 1f && health.currentMana > 0f)
            {
                health.UseMana(10f);
                SpellCountDown();
            }
            else
            {
                Debug.Log("CD time");
            }
        }


        if (canMove)
		{
			ControlPlayer();
            if (Input.GetMouseButtonDown(0) && animTime == 0)
            {
			    RotatePlayer();
                anim.SetBool("attack", true);
			    canMove = false;
                sword.enabled = true;
            }
		}
        
        if (!canMove)
		{
			animTime += Time.deltaTime;
			
			if(animTime >= attackTime*0.66)
			{
				anim.SetBool("attack", false);
				canMove = true;
                sword.enabled = false;
                animTime = 0;
            }
        }

        if(spellCount < 1f)
        {
            spellCount += Time.deltaTime;
            health.SpellCD.fillAmount = spellCount;
        }
    }

    void SpellCountDown()
    {
        spellCount = 0f;
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
		Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
		float rayLength;
        Vector3 pointToLook;
		if(groundPlane.Raycast(ray, out rayLength))
		{
			pointToLook = ray.GetPoint(rayLength);
			transform.LookAt(pointToLook);
		}
	}
}
