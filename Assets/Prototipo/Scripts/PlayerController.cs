using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	
	[SerializeField]
	float movementSpeed = 0.0f;
	void Update(){
		ControlPlayer();
	}
	void ControlPlayer(){
		float moveHorizonat = Input.GetAxisRaw("Horizontal");
		float moveVertical = Input.GetAxisRaw("Vertical");

		Vector3 movement = new Vector3(moveHorizonat, 0.0f, moveVertical);
		//transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15F);
		if(movement != Vector3.zero) transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement.normalized), 0.2f);
		transform.Translate (movement*movementSpeed*Time.deltaTime, Space.World);
	}

/*
	public float moveSpeed;
	private Camera mainCamera;
	private Rigidbody rb;

	private Vector3 moveInput;
	private Vector3 moveVelocity;

	void Start(){
		rb = GetComponent<Rigidbody>();
		mainCamera = FindObjectOfType<Camera>();
	}

	// Update is called once per frame
	void Update () {


		moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
		moveVelocity = moveInput * moveSpeed;

		

		
		Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
		Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
		float rayLength;

		if(groundPlane.Raycast(cameraRay, out rayLength)){
			Vector3 pointToLook = cameraRay.GetPoint(rayLength);
			Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);

			transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
		}

	}

	void FixedUpdate(){
		rb.velocity = moveVelocity;
	}
*/
}
