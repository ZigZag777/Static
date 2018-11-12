using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim : MonoBehaviour {

	private string spriteNamesOpen = "Player/Player";
    private SpriteRenderer spriteR;
    private Sprite[] sprites;
	private float dx1,dy1;

	// Use this for initialization
	void Start () {
		spriteR = gameObject.GetComponent<SpriteRenderer>();
        sprites = Resources.LoadAll<Sprite>(spriteNamesOpen);
		spriteR.sprite = sprites[3]; 

		//dx=gameObject.GetComponent<move>().dx;
		//dy=gameObject.GetComponent<move>().dy;
	}
	
	// Update is called once per frame
	void Update () {
		 
		if (gameObject.GetComponent<move>().CanMove){
		if (Input.GetAxis("Horizontal")>0){

			spriteR.sprite = sprites[2];
		}
		if (Input.GetAxis("Horizontal")<0){
			spriteR.sprite = sprites[0];
		}
		if (Input.GetAxis("Horizontal")==0 & Input.GetAxis("Vertical")>0){
			
			spriteR.sprite = sprites[1];
		}
		if (Input.GetAxis("Horizontal")==0 & Input.GetAxis("Vertical")<0){
			
			spriteR.sprite = sprites[3];
		}
		}

	}

	/*
	void LateUpdate()
	{
		dx1=gameObject.GetComponent<move>().dx;
		dy1=gameObject.GetComponent<move>().dy;
		print(dx1);
		if (dx1>0){
			print("!!!");
			spriteR.sprite = sprites[2];
		}
		if (dx1<0){
			spriteR.sprite = sprites[0];
		}
		
	} */
}
