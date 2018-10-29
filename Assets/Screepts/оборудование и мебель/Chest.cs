using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour {

	private string spriteNamesOpen = "Oборудование/Сундук/Сундук";
    private SpriteRenderer spriteR;
    private Sprite[] sprites;
	// Use this for initialization
	void Start () {
		spriteR = gameObject.GetComponent<SpriteRenderer>();
        sprites = Resources.LoadAll<Sprite>(spriteNamesOpen);
		spriteR.sprite = sprites[0]; 
	}
	
	// Update is called once per frame
	void Update () {
		//if (Input.GetKey(KeyCode.I)){
		//	spriteR.sprite = sprites[1];
		//}
		//Chest1();
	}
	void OnGUI()
    {        
	   //	spriteR.sprite = sprites[0];   
    }
	public void ChestOpen(){
		spriteR.sprite = sprites[1]; 
	}

	public void ChestClose(){
		spriteR.sprite = sprites[0]; 
	}
}
