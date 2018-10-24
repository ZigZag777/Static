using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

	public GameObject inventory;
	public GameObject container;
	private Item item;
	bool checkinv = false;  // проверка инвентаря
	//private int CountFullCell;
	public	List<Item> list; 
	// Use this for initialization
	private
	void Start () {
		list = new List<Item>(); //
		//print(list.Count.ToString());
	}

	int CEC(){			// count empty cells возвращает количество пустых ячеек
		
		return inventory.transform.childCount-list.Count;
	}

	int CECFR(string NameRes,int MaxStaсk){ 	//Count empty cells for resourses возвращает количество русурсов с именем NameRes, которые можно засунуть в инвентарь
												//входит имя параметра и его размер максимального стака
				
		int rest=0;
		for (int i=0;i<list.Count;i++){
				if (list[i].NameRes==NameRes & list[i].count!=MaxStaсk){
					rest+=MaxStaсk-list[i].count;
				}
		}
		
		return rest+CEC()*MaxStaсk;
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log(inventory.transform.GetChild(0).childCount);
		//Debug.Log(list.Count);

		if (list.Count>0 ){  // кусок для проверки, не функционален
		//	Debug.Log(list[0].count);
		}//

		
		/* начало старого куска
		if (Input.GetKeyDown(KeyCode.I)){
			if (inventory.activeSelf){
				checkinv = false;
				inventory.SetActive(false);
				for (int i=0;i<inventory.transform.childCount;i++){
					if (inventory.transform.GetChild(i).transform.childCount>0){
						Destroy(inventory.transform.GetChild(i).transform.GetChild(0).gameObject);
					}
				}
			}
			else{
				checkinv = true;
				inventory.SetActive(true);
				int count =list.Count;
				for (int i=0;i<count;i++){
					Item it=list[i];
					if (inventory.transform.childCount>=i){
						
						if (inventory.transform.GetChild(i).childCount<1){
						GameObject img = Instantiate(container);
						img.transform.SetParent(inventory.transform.GetChild(i).transform);
						img.GetComponent<Image>().sprite=Resources.Load<Sprite>(it.sprite);
						}
					}
					else break;
				}
			}
			
		}
		*/ //конец старого куска



		/* начало нового куска
		if (Input.GetKeyDown(KeyCode.I)){
			if (checkinv){
				checkinv=false;
			}
			else{
				checkinv=true;
			}
		}

		/*
		if (Input.GetKeyDown(KeyCode.I)){
			if (inventory.activeSelf){
				checkinv = false;
				inventory.SetActive(false);
				for (int i=0;i<inventory.transform.childCount;i++){
					if (inventory.transform.GetChild(i).transform.childCount>0){
						Destroy(inventory.transform.GetChild(i).transform.GetChild(0).gameObject);
					}
				}
			}
			*/
			/*
			if(checkinv) {
				checkinv = true;
				inventory.SetActive(true);
				int count =list.Count;
				for (int i=0;i<count;i++){
					Item it=list[i];
					if (inventory.transform.childCount>=i){
						
						if (inventory.transform.GetChild(i).childCount<1){
						GameObject img = Instantiate(container);
						img.transform.SetParent(inventory.transform.GetChild(i).transform);
						img.GetComponent<Image>().sprite=Resources.Load<Sprite>(it.sprite);
						}
						int countres= it.count;
						if (inventory.transform.GetChild(i).GetChild(0).childCount>0){
						inventory.transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Text>().text=countres.ToString();
						}
					}
					//else break;
				}
			}
			
		//} */   // конец нового куска
			
			
			
			//динамическое обновление инвенторя
			/*
			if (checkinv){
				int count =list.Count;
				for (int i=0;i<count;i++){
					Item it=list[i];
					int countres= it.count;
					if (inventory.transform.GetChild(i).GetChild(0).childCount>0){
						inventory.transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Text>().text=countres.ToString();
					}
				}
			}
			*/


			

	if (Input.GetKeyDown(KeyCode.I)){
		if (checkinv==false) {
			OpenInventory();
		} 
		else{
			CloseInventory();
		}

	}


	}


void OpenInventory(){
	inventory.SetActive(true);
	checkinv=true;
	int count =list.Count;
	for (int i=0;i<count;i++){
		Item it=list[i];
		if (inventory.transform.childCount>=i){			
				
				GameObject img = Instantiate(container);
				img.transform.SetParent(inventory.transform.GetChild(i).transform);
				img.GetComponent<Image>().sprite=Resources.Load<Sprite>(it.sprite);
				inventory.transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Text>().text=it.count.ToString();
		}
		else break;
	}
}

void CloseInventory(){
	inventory.SetActive(false);
	checkinv=false;
	for (int i=0;i<inventory.transform.childCount;i++){
		if (inventory.transform.GetChild(i).transform.childCount>0){
			Destroy(inventory.transform.GetChild(i).transform.GetChild(0).gameObject);
		}
	}
	
}

void OnTriggerEnter2D(Collider2D other) // подбираем предмет
{	
	//print(CECFR(other.GetComponent<Item>().NameRes, other.GetComponent<Item>().MaxStaсk)); 
	int CountEmpty=0;
	CountEmpty=CECFR(other.GetComponent<Item>().NameRes, other.GetComponent<Item>().MaxStaсk);	// колво свободного места в инвентаре
	
	if (CountEmpty>other.GetComponent<Item>().count){		//проверяем хватает ли места для стака
		PootToInventory(other.GetComponent<Item>());		//если хватает запескаем процедуру завовывания в инвентарь
	Destroy(other.gameObject);
	}
	else {							
		if (CountEmpty>0){									// если не хватает, 
			int rest=other.GetComponent<Item>().count-CountEmpty;
			other.GetComponent<Item>().count=CountEmpty;
			PootToInventory(other.GetComponent<Item>());
			other.GetComponent<Item>().count=rest;
		}

	}
}

void PootToInventory(Item item){		// засовывание ресурса в инвентарь, на входе итем ресурса
	bool checkrepeat = false;
	int count =list.Count;
	int rest=0;
	//item = other.GetComponent<Item>(); 	//итем подобранного предмета
	if (item!=null){    				//Проверяем есть ли что нибудь в инвентаре
	for (int i=0;i<count;i++){			//бежим по инвентарю
		
		if (list[i].prefab==item.prefab & list[i].count!=item.MaxStaсk){	//если нашли в инвентаре что уже было, то стакаем
			checkrepeat=true;
			 if (list[i].count+item.count+rest<=list[i].MaxStaсk){		//проверяем можно ли засунуть в стак
				 list[i].count+=item.count+rest;						//если получилось уходим
				 i=count;
			 }
			 else{
				rest=list[i].count+item.count-list[i].MaxStaсk;
				list[i].count=list[i].MaxStaсk;
				item.count=rest;				
			 }
		}
	}	
		if (checkrepeat!=true ^ rest!=0 ){				//если не нашли добавляем новый
			list.Add(item);
		}

	}

}


 
}
