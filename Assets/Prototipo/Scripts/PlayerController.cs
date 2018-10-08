using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	
	private Rigidbody rb;
	private Animator anim;

	[SerializeField]
	float movementSpeed = 0.0f;

	void Start(){
		rb = GetComponent<Rigidbody>();
		anim = GetComponent<Animator>();
	}
	void Update(){
		ControlPlayer();
	}
	void ControlPlayer(){
		float moveHorizonat = Input.GetAxisRaw("Horizontal");
		float moveVertical = Input.GetAxisRaw("Vertical");
		
		Vector3 movement = new Vector3(moveHorizonat, 0.0f, moveVertical);
		if(movement != Vector3.zero) transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement.normalized), 0.2f);
		transform.Translate (movement*movementSpeed*Time.deltaTime, Space.World);

		if(movement != Vector3.zero){
			anim.SetBool("moving", true);
		}else{
			anim.SetBool("moving", false);
		}
	}
}
