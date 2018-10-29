using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryOther : MonoBehaviour {
	
	public GameObject InventoryOthrePanel;
	public GameObject ContainerOther;
	private Item item;
	public	List<Item> list; 
	
	// Use this for initialization
	void Start () {
		list = new List<Item>(); 
		for(int i=0;i<InventoryOthrePanel.transform.childCount;i++) {
			InventoryOthrePanel.transform.GetChild(i).GetComponent<Cell>().Number=i;
		}
		
	}

		int CEC(){			// count empty cells возвращает количество пустых ячеек
		
		return InventoryOthrePanel.transform.childCount-list.Count;
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

	int MinItem(){							//функция поиска первого возможного индекса итема в инвернтаре, который можно заполнить
		int Min=list.Count;
		if (Min==0){
			return 0;
		}
		for (int i=0; i<InventoryOthrePanel.transform.childCount;i++){

			if (InventoryOthrePanel.transform.GetChild(i).GetComponent<Cell>().IsEmpty){
				
				Min=i;
				break;
				//print(item.name);
			}
		}
		return Min;
	}	
	
	// Update is called once per frame
	void Update () {
		
	}
	public void OpenInventoryOther(){
		InventoryOthrePanel.SetActive(true);

		int count =list.Count;
		for (int i=0;i<count;i++){
			Item it=list[i];
			if (InventoryOthrePanel.transform.childCount>=i){				
				GameObject img = Instantiate(ContainerOther);
				img.transform.SetParent(InventoryOthrePanel.transform.GetChild(i).transform);
				img.GetComponent<Image>().sprite=Resources.Load<Sprite>(it.sprite);
				img.GetComponent<Slot>().item=it;
				InventoryOthrePanel.transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Text>().text=it.count.ToString();
			}	
			else break;
		}

	}
	public void CloseInventoryOther(){
		InventoryOthrePanel.SetActive(false);
		for (int i=0;i<InventoryOthrePanel.transform.childCount;i++){
			if (InventoryOthrePanel.transform.GetChild(i).transform.childCount>0){
				Destroy(InventoryOthrePanel.transform.GetChild(i).transform.GetChild(0).gameObject);
			}	
		}

	}
}
