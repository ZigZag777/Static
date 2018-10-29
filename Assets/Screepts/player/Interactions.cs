using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactions : MonoBehaviour {

	private GameObject Panel;	//Панель инвентаря героя
	private InventoryDraw Draw;	//ловим скрипт холдера 
	private	List<Item> ListItem; 		//массив объектов инвентаря
	private Inventory Inv;				//Ловим скрипт Inventory
	private string OtherTag="None";		//таг объекта к которому подошли

	
	void Start(){
		Draw=GameObject.Find("_Holder").GetComponent<InventoryDraw>(); //ловим скрипт холдера
		Panel=gameObject.GetComponent<Inventory>().Panel;
		ListItem = new List<Item>();
		Inv=gameObject.GetComponent<Inventory>();
	}

	void Update(){
		if (Input.GetKeyDown(KeyCode.I)){
			ListItem=Inv.list;
			Draw.InitInventory(Panel,ListItem);
		}
	}
	void OnTriggerEnter2D(Collider2D other) // подбираем предмет
	{	

		OtherTag="None";
		OtherTag=other.tag;
		switch (OtherTag){
        	case "Chest":
				/*
				var color1 = spriteTriger.color;	//показываем меточку
				color1.a=1;
				spriteTriger.color=color1;
				*/
				other.GetComponent<Chest>().ChestOpen();	// открываем сундук

				break;
			case "item":{
				int CountEmpty=0;
				//CountEmpty=Inv.CECFR(other.GetComponent<Item>().NameRes, other.GetComponent<Item>().MaxStaсk);	// колво свободного места в инвентаре
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
		OtherTag="None";
		OtherTag=other.tag;
		switch (OtherTag){
        	case "Chest":
				/* 
				var color1 = spriteTriger.color; //скрываем меточку
				color1.a=0;
				spriteTriger.color=color1;
				*/
				other.GetComponent<Chest>().ChestClose();	// закрываем сундук
		
				if (other.GetComponent<Inventory>().Panel.activeSelf){
					other.GetComponent<InventoryOther>().CloseInventoryOther();
					//gameObject.GetComponent<Inventory>().CloseInventory();	
				}

				break;
		}
		//gameObject.GetComponent<Inventory>().CloseInventory();
		//OtherObj.GetComponent<InventoryOther>().CloseInventoryOther();
		OtherTag="None";
	}
}
