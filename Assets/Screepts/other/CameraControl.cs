using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
	public Transform target;
	private Vector3 offset;
	//private Vector3
	// Use this for initialization
	void Start () {
		//offset = transform.position = Player.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.position = new Vector3(0,0,-500)+target.position;
		transform.LookAt(target);
	}
}
