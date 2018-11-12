using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OlmaZindex : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		var scale=transform.localScale;
		if (GameObject.Find("Player").transform.position.x<gameObject.transform.position.x){
			scale.x=-2;
			transform.localScale=scale;
		}
		else{
			scale.x=2;
			transform.localScale=scale;
		}
	}
}
