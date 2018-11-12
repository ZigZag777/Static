using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour {

	public Rigidbody2D PLayerForce;
	public GameObject inventory;
	public float PlayerSpeed;
	public Animator HeroAmim;
	public bool CanMove;

	
	// Use this for initialization
	public float dx,dy;
	public SpriteRenderer spriteTriger;
	void Start () {
		CanMove=true;
		PLayerForce = GetComponent<Rigidbody2D>();
		//spriteTriger=GetComponent<SpriteRenderer>();
		
	}
	
	// Update is called once per frame
	void Update () {
		//print(Time.frameCount / Time.time);
		if (CanMove){
		dirvelocity();
		if (Input.GetKey (KeyCode.W)) 
		{
			//sbrosAnim();
			//HeroAmim.SetBool ("Up",true);
			PLayerForce.velocity=new Vector2(dx,dy);
		}

		if (Input.GetKey (KeyCode.S)) 
		{
			//sbrosAnim();
			//HeroAmim.SetBool ("Down",true);	
			PLayerForce.velocity=new Vector2(dx,dy);
		}

		if (Input.GetKey (KeyCode.A)) 
		{
			//sbrosAnim();
			//HeroAmim.SetBool ("Left",true);	
			PLayerForce.velocity=new Vector2(dx,dy);
		}
		if (Input.GetKey (KeyCode.D)) 
		{
			//sbrosAnim();
			//HeroAmim.SetBool ("Right",true);
			PLayerForce.velocity=new Vector2(dx,dy);
		}
		if (Input.anyKey == false) {
			//sbrosAnim();
			PLayerForce.velocity=new Vector2(0,0);
		}
		}
	}


void FixedUpdate()
{

}
void OnTriggerEnter2D(Collider2D other)
{
	//Debug.Log(other.tag);
	//var color1 = spriteTriger.color;
	//color1.a=1;
	//spriteTriger.color=color1;
}


void OnTriggerExit2D(Collider2D other)
{
	//var color1 = spriteTriger.color;
	//color1.a=0;
	//spriteTriger.color=color1;
}

	void sbrosAnim() {
		
		HeroAmim.SetBool ("Left",false);	
		HeroAmim.SetBool ("Right",false);	
		HeroAmim.SetBool ("Up",false);
		HeroAmim.SetBool ("Down",false);
	}

void dirvelocity()
{
	if (Mathf.Abs(Input.GetAxis("Horizontal")) + Mathf.Abs(Input.GetAxis("Vertical"))==2)
	{
		dx=Input.GetAxis("Horizontal")*0.7f*PlayerSpeed;
		dy=Input.GetAxis("Vertical")*0.7f*PlayerSpeed;
	}
	else{
		dx=Input.GetAxis("Horizontal")*PlayerSpeed;
		dy=Input.GetAxis("Vertical")*PlayerSpeed;
	}
}

}
