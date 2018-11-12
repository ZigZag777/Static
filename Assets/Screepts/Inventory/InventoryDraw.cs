using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDraw : MonoBehaviour {

	public GameObject Panel;		//панель нашего инвентаря
	public	List<Item> ListItem; 		//массив объектов инвентаря
	private Item item;				//Элемент массива инвентаря
	public GameObject container;
 	public bool NeedUpdate=false;	//метка обозначающая необходимость обновить инвентарь, чекается при поднятии или перетаскивания предмета 

	
	/*
	public void InitInventory(GameObject panel, List<Item> list){
		Panel=panel;
		ListItem=list;
		if (Panel.activeSelf==false) {
			OpenInventory();
		} 
		else{
			CloseInventory();
		}
	}
	*/
	public void OpenInventory(GameObject panel, List<Item> list){
		Panel=panel;
		ListItem=list;
		Panel.SetActive(true);
		UpdateInventory(Panel, ListItem);
	}
	public void CloseInventory(GameObject panel){
		Panel=panel;
		Panel.SetActive(false);
		for (int i=0;i<Panel.transform.childCount;i++){
			if (Panel.transform.GetChild(i).transform.childCount>0){
			Destroy(Panel.transform.GetChild(i).transform.GetChild(0).gameObject);
		}
	}
	}

	public void UpdateInventory (GameObject panel, List<Item> list){
		Panel=panel;
		ListItem=list;
		for (int k=0;k<Panel.transform.childCount;k++){
			if (Panel.transform.GetChild(k).transform.childCount>0){
				Destroy(Panel.transform.GetChild(k).transform.GetChild(0).gameObject);
			}
		}
		NeedUpdate=true;
	}

	void LateUpdate()
	{
		if (NeedUpdate){
			int NumberCell;
			int count =ListItem.Count;
			for (int j=0;j<Panel.transform.childCount;j++){
				NumberCell=Panel.transform.GetChild(j).GetComponent<Cell>().Number;
					for (int i=0;i<count;i++){
			 			Item it=ListItem[i];
						if (NumberCell==it.Number){
							if (Panel.transform.childCount>=i){			
								GameObject img = Instantiate(container);
								img.transform.SetParent(Panel.transform.GetChild(j).transform);
								img.GetComponent<Image>().sprite=Resources.Load<Sprite>(it.sprite);
								img.GetComponent<Slot>().item=it;
								img.GetComponent<Slot>().Number=i;
								Panel.transform.GetChild(j).GetChild(0).GetChild(0).GetComponent<Text>().text=it.count.ToString();
							}
						}
			
					}
			} 
			NeedUpdate=false;
		}
	}

}
