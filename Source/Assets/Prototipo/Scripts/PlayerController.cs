using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	
	private Rigidbody rb;
	private Animator anim;
	public AnimationClip attackAnim;
	private float animTime;
	private float attackTime;
	private bool canMove;

	[SerializeField]
	float movementSpeed = 4.0f;

	void Start(){
		rb = GetComponent<Rigidbody>();
		anim = GetComponent<Animator>();
		attackTime = attackAnim.length;
		canMove = true;
	}

	void Update(){
		if(canMove)
		{
			ControlPlayer();
		}

        if (Input.GetMouseButtonDown(0) && animTime == 0)
        {
			RotatePlayer();
            anim.SetBool("attack", true);
			canMove = false;
        }
        
        if (!canMove)
		{
			animTime += Time.deltaTime;
			
			if(animTime >= attackTime*0.5)
			{
				anim.SetBool("attack", false);
				canMove = true;
				animTime = 0;
			}
        }
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
		if(groundPlane.Raycast(ray, out rayLength))
		{
			Vector3 pointToLook = ray.GetPoint(rayLength);
			transform.LookAt(pointToLook);
		}
	}
}
