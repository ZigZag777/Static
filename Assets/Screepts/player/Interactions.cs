using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactions : MonoBehaviour {
	private float timeRemaining = 1f;

	private GameObject Panel;	//Панель инвентаря героя
	private InventoryDraw Draw;	//ловим скрипт холдера 
	private	List<Item> ListItem; 		//массив объектов инвентаря
	private Inventory Inv;				//Ловим скрипт Inventory
	//private string OtherTag="None";		//таг объекта к которому подошли
	public GameObject OtherObj=null;
	public bool needUpdateChestInv=false;

	
	void Start(){
		PlayerPrefs.DeleteAll();  
		Draw=GameObject.Find("_Holder").GetComponent<InventoryDraw>(); //ловим скрипт холдера
		Panel=gameObject.GetComponent<Inventory>().Panel;
		Inv=gameObject.GetComponent<Inventory>();
		ListItem=Inv.list;
		
	}

	void Update(){

		if (Input.GetKeyDown(KeyCode.Escape)){
			Application.Quit();
		}
		
		if (needUpdateChestInv){
			if (OtherObj.GetComponent<Inventory>().Panel.activeSelf){
				Draw.CloseInventory(OtherObj.GetComponent<Inventory>().Panel);
			}
			else{
				Draw.OpenInventory(OtherObj.GetComponent<Inventory>().Panel, OtherObj.GetComponent<Inventory>().list);
			}
			needUpdateChestInv=false;
		}
		
		if (Input.GetKeyDown(KeyCode.I)){
			
			if (Panel.activeSelf){
				Draw.CloseInventory(Panel);
			}
			else{
				Draw.OpenInventory(Panel,ListItem);
			}	
		}
		if (Input.GetKeyDown(KeyCode.E)){
			if (OtherObj!=null){
				if(OtherObj.tag=="Chest"){
					OtherObj.GetComponent<Chest>().InitInventoryChest();
					
					
					if (OtherObj.GetComponent<Inventory>().Panel.activeSelf==false &Panel.activeSelf){	//проверяем открыт ли у нас УЖЕ инвентарь
						OtherObj.GetComponent<Chest>().ChestOpen();										//если да, то только открываем инвентарь сундука
						needUpdateChestInv=true;
						return;
					}	
					else{
						
					}
					if (Panel.activeSelf){
						Draw.CloseInventory(Panel);
					}
					else{
						Draw.OpenInventory(Panel,ListItem);
					}
					needUpdateChestInv=true;			//обновляем инвентарь сундука в сл шаге
				}
			}
		}

		if (Input.GetKey(KeyCode.E)){
			
			if (OtherObj!=null){
				timeRemaining-=Time.deltaTime;
				//print(timeRemaining);
				if ( timeRemaining < 0 ) {
					Use(OtherObj);
					timeRemaining=1f;
				}
			}
		}
		if (Input.GetKeyUp(KeyCode.E)){
			timeRemaining=1f;
		}
	}
	void OnTriggerEnter2D(Collider2D other) // подбираем предмет
	{	

		OtherObj=other.gameObject;

		switch (other.tag){
        	case "Chest":
				/*
				var color1 = spriteTriger.color;	//показываем меточку
				color1.a=1;
				spriteTriger.color=color1;
				*/



				break;
			case "item":{
				int CountEmpty=0;
				CountEmpty=Inv.CECFR(other.GetComponent<Item>());	// колво свободного места в инвентаре
	
				if (CountEmpty>other.GetComponent<Item>().count){		//проверяем хватает ли места для стака
					Inv.PootToInventory(other.GetComponent<Item>());		//если хватает запускаем процедуру засовывания в инвентарь
					Destroy(other.gameObject);
				}
				else {							
					if (CountEmpty>0){									// если не хватает, 
						int rest=other.GetComponent<Item>().count-CountEmpty;
						other.GetComponent<Item>().count=CountEmpty;
						Inv.PootToInventory(other.GetComponent<Item>());
						other.GetComponent<Item>().count=rest;
					}
				}

				break;
				}
		}

	}

	void OnTriggerExit2D(Collider2D other)
	{
		OtherObj=other.gameObject;
		switch (other.tag){
        	case "Chest":
				/* 
				var color1 = spriteTriger.color; //скрываем меточку
				color1.a=0;
				spriteTriger.color=color1;
				*/
				other.GetComponent<Chest>().ChestClose();	// закрываем сундук

				if (other.GetComponent<Inventory>().Panel.activeSelf){		//закрываем панели если надо
					Draw.CloseInventory(Panel);
					Draw.CloseInventory(OtherObj.GetComponent<Inventory>().Panel);
	
				}
				
				break;
		}

		OtherObj=null;
	}

	private void Use(GameObject Other){

		//string UsingName="Using"+Other.gameObject.transform.parent.name;
		//print(UsingName);
		//Other.gameObject.transform.parent.comp	GetComponent<UsingName>().use();

		Other.transform.parent.GetComponent<UsingTree>().use();

	}
}
