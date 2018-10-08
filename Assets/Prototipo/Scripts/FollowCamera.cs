using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {

	public Transform PlayerTransform;

	private Vector3 offset;

	[Range(0.01f, 1.0f)]
	public float Smooth = 0.5f;

	// Use this for initialization
	void Start () {
		offset = transform.position - PlayerTransform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		Vector3 newPos = PlayerTransform.position + offset;

		transform.position = Vector3.Slerp(transform.position, newPos, Smooth);
	}
}
