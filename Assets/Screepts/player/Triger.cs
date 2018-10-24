using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triger : MonoBehaviour {

	public SpriteRenderer spriteTriger;
	private Color Color1;
	public GameObject tri;
	public float col;

	// Use this for initialization
	void Start () {
		spriteTriger=GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
	var color = spriteTriger.color;
	color.a=0;
			if (Input.GetMouseButton(0)){
			spriteTriger.color=color;
		}	
		
		//Debug.Log(col.ToString);
	}
}
