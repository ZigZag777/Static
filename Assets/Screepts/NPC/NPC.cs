using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour {
	public bool OlmaGoParty=false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (OlmaGoParty){
			GameObject.Find("Text").GetComponent<Text>().text = "Ольма ИДЁТ на вечеринку";
		}
		else{
			GameObject.Find("Text").GetComponent<Text>().text = "Ольма НЕ идёт на вечеринку";
		}
	}

}
