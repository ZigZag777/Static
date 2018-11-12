using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour {

	public string spriteNamesOpen = "Oборудование/Сундук/Сундук";	//самая важная строка, в неё добавляем путь к спрайту 
    public SpriteRenderer spriteR;
    private Sprite[] sprites;
	private InventoryDraw Draw;	//ловим скрипт холдера 

	private GameObject Panel;			//панель нашего инвентаря

	private	List<Item> ListItem; 		//массив объектов инвентаря сундука
	// Use this for initialization
	void Start () {
		spriteR = gameObject.GetComponent<SpriteRenderer>();
        sprites = Resources.LoadAll<Sprite>(spriteNamesOpen);
		spriteR.sprite = sprites[0]; 
		Panel=gameObject.GetComponent<Inventory>().Panel;
		ListItem=gameObject.GetComponent<Inventory>().list;
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


	public void InitInventoryChest(){
		if (spriteR.sprite == sprites[1]){
			spriteR.sprite = sprites[0];
		}
		else{
			spriteR.sprite = sprites[1];
		}

	}

	public void ChestOpen(){
		spriteR.sprite = sprites[1]; 
		//Draw.InitInventory(Panel,ListItem);

	}

	public void ChestClose(){
		spriteR.sprite = sprites[0]; 
	}
}
