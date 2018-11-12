using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triger : MonoBehaviour {
	private GameObject Label;
	public bool CanUse;
	// Use this for initialization
	void Start () {
		Label=gameObject.transform.parent.Find("Label").gameObject;
		//Triger=gameObject.transform.parent.Find("Triger").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		Label.SetActive(true);
		CanUse=true;
		
	}

	
	void OnTriggerExit2D(Collider2D other)
	{
		Label.SetActive(false);
		CanUse=false;
		
	}
}