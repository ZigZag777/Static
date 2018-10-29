using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
	public GameObject Panel;		//панель нашего инвентаря
	public	List<Item> list; 		//массив объектов инвентаря
	private Item item;				//Элемент массива инвентаря
	public bool NeedUpdate=false;	//метка обозначающая необходимость обновить инвентарь, чекается при поднятии или перетаскивания предмета 
	private InventoryDraw Draw;	//ловим скрипт холдера 
	void Start () {
		Draw=GameObject.Find("_Holder").GetComponent<InventoryDraw>(); //ловим скрипт холдера
		list = new List<Item>(); //создаём массив итемов
		//print (list.Count);
		for(int i=0;i<Panel.transform.childCount;i++) {
			Panel.transform.GetChild(i).GetComponent<Cell>().Number=i;
		}
		
	}
	int MinItem(){							//функция поиска первого возможного индекса итема в инвернтаре, который можно заполнить
		int Min=list.Count;
		if (Min==0){
			return 0;
		}
		for (int i=0; i<Panel.transform.childCount;i++){

			if (Panel.transform.GetChild(i).GetComponent<Cell>().IsEmpty){
				
				Min=i;
				break;
				//print(item.name);
			}
		}
		return Min;
	}
	int CEC(){			// count empty cells возвращает количество пустых ячеек
		
		return Panel.transform.childCount-list.Count;
	}

	public int CECFR(Item it){ 	//Count empty cells for resourses возвращает количество русурсов плжлюоанного айтема, которые можно засунуть в инвентарь
												//входит айтем предмета
				
		int rest=0;
		for (int i=0;i<list.Count;i++){
				if (list[i].NameRes==it.NameRes & list[i].count!=it.MaxStaсk){
					rest+=it.MaxStaсk-list[i].count;
				}
		}
		return rest+CEC()*it.MaxStaсk;
	}	

	public void PootToInventory(Item item){	//засовываем айтем в инвентарь
		bool checkrepeat = false;
		int count =list.Count;
		int rest=0;
		//item = other.GetComponent<Item>(); 	//итем подобранного предмета
		if (item!=null){    				//Проверяем нормальный ли итем пришёл
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
				item.Number=MinItem();
				Panel.transform.GetChild(MinItem()).GetComponent<Cell>().IsEmpty=false;
				list.Add(item);
			}

		}
		if (Panel.activeSelf){
			Draw.UpdateInventory(Panel,list);
		}
	}
}
