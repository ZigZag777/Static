using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Drop : MonoBehaviour,IDropHandler     {                             //,IPointerClickHandler {

	
	private int numberItem;
	
	public	List<Item> ListItem; 			// наш инвентарь
	//private GameObject PanelOther;
	//public	List<Item> ListItemOther; 		//инвентарь сундука
	private InventoryDraw Draw;	//ловим скрипт холдера 
	private bool NeedUpdate =false;	//метка когда нужно обновить два инвентаря кряду, второй обнавляем только в следующем шаге (((( ничего не придумал другого.


	// Use this for initialization
	void Start () {
		Draw=GameObject.Find("_Holder").GetComponent<InventoryDraw>(); //ловим скрипт холдера
		//ListItem=GameObject.Find("Player").GetComponent<Inventory>().list;
	}
	
	// Update is called once per frame
	void Update () {
		GameObject PanelNotUse=GameObject.Find("Player").GetComponent<Inventory>().Panel;
		List<Item> ListNotUse=GameObject.Find("Player").GetComponent<Inventory>().list;
		if (NeedUpdate){
			print("обнавляем инвентарь героя");
			Draw.UpdateInventory(PanelNotUse,ListNotUse);
			NeedUpdate=false;
		}

	}


public void OnDrop(PointerEventData eventData){
		
		Drag drag1 =eventData.pointerDrag.GetComponent<Drag>();
		GameObject PanelPlayer=transform.parent.gameObject;
		//GameObject PanelOther;
		string OldPanelName=drag1.oldcanvas.parent.name;
		List<Item> ListPleer=GameObject.Find("Player").GetComponent<Inventory>().list;
		
		//print(PanelName);										//имя панели ИЗ КОТОРОЙ перетаскиваем
		//print(OldPanelName);									//имя панели В КОТОРУЮ перетаскиваем
		if(PanelPlayer.name==OldPanelName){							//если перетаскиваем ВНУТРИ одного инвентаря
			
				if (PanelPlayer.name=="Inventory") {								// таскаем внутри инвертаря героя
					ListItem=ListPleer;	// присваеваем листу массив итемов героя
					if (drag1!=null){							//проверяем таскаем ли мы что нибудь
						if (transform.childCount>0){			//проверяем заполненость ячейки в которую положили
							dragInNotEmpty(eventData,PanelPlayer);			//процедура, если заполнена
						}
						else{
							dragInEmpty(eventData);					//процедура если не заполнена
						}
					}
				}
				else{											//таскаем внутри инвентаря сундука
					List<Item> ListOther=GameObject.Find("Player").GetComponent<Interactions>().OtherObj.GetComponent<Inventory>().list;	//ловим итем сундука
					ListItem=ListOther;	//присваеваем листу массив элементов сундука
					if (drag1!=null){							//проверяем таскаем ли мы что нибудь
						if (transform.childCount>0){			//проверяем заполненость ячейки в которую положили
							dragInNotEmpty(eventData,PanelPlayer);			//процедура, если заполнена
						}
						else{
							dragInEmpty(eventData);					//процедура если не заполнена
						}
					}
				}
			
			
		}
		else{											// перекидываем МЕЖДУ разными инвентарями
			List<Item> ListOther=GameObject.Find("Player").GetComponent<Interactions>().OtherObj.GetComponent<Inventory>().list;
			GameObject PanelOther=GameObject.Find("Player").GetComponent<Interactions>().OtherObj.GetComponent<Inventory>().Panel;	//ловим панель сундука
			if (drag1!=null){							//проверяем таскаем ли мы что нибудь
				if (transform.childCount>0){			//проверяем заполненость ячейки в которую положили
					if (OldPanelName=="Inventory"){		//перетаскиваем из инвентаря в сундук
						dragInNotEmptyOther(eventData,ListPleer,ListOther,PanelPlayer,PanelOther);
//						print("Из инвентаря в сундук");
					}
					else{								//перетаскиваем из сундука в инвентарь
						dragInNotEmptyOther(eventData,ListOther,ListPleer,PanelOther,PanelPlayer);
//						print("Из сундука в инвентарь");
					}
				}
				else{
					if (OldPanelName=="Inventory"){		//перетаскиваем из инвентаря в сундук
						dragInEmptyOther(eventData,ListPleer,ListOther);
					}
					else{								//перетаскиваем из сундука в инвентарь
						dragInEmptyOther(eventData,ListOther,ListPleer);
					}
				}
			}
		}
	}

	void dragInEmpty(PointerEventData eventData) {		
		Drag drag =eventData.pointerDrag.GetComponent<Drag>();		//присваеваем драгу то что таскаем
		drag.transform.SetParent(transform);						//закидываем в новую ячейку
		drag.transform.parent.GetComponent<Cell>().IsEmpty=false;	//Заполняем ячейку В КОТОРУЮ закинули
		drag.oldcanvas.GetComponent<Cell>().IsEmpty=true;			// убираем заполненость ячейки ИЗ КОТОРОЙ вытащили
	
		for (int i=0;i<ListItem.Count;i++){								//ищем итем с номером ячейки ИЗ КОТОРОЙ перетащили
			if (ListItem[i].Number==drag.oldcanvas.GetComponent<Cell>().Number){
				numberItem=i;
				break;
			}
		}
		ListItem[numberItem].Number=drag.transform.parent.GetComponent<Cell>().Number;	//присваеваем переменной "номер" элемента лист, номер ячейки куда мы закинули	
	}

	void dragInNotEmpty(PointerEventData eventData,GameObject Panel) {
		Drag drag =eventData.pointerDrag.GetComponent<Drag>();		//присваеваем драгу то что таскаем
		drag.transform.SetParent(transform);						//закидываем в новую ячейку
		transform.GetChild(0).SetParent(drag.oldcanvas);			//перетаскиваем предмет из ячейки в которую закинули, в старую ячейку


		for (int i=0;i<ListItem.Count;i++){							//ищем итем с номером ячейки ИЗ КОТОРОЙ перетащили
			if (ListItem[i].Number==drag.oldcanvas.GetComponent<Cell>().Number){
				numberItem=i;
				break;
			}
		}
		int NumOld=numberItem;										//запоминаем номер ячейки из которой взяли

		for (int i=0;i<ListItem.Count;i++){							//ищем итем с номером ячейки В КОТОРУЮ перетащили
			if (ListItem[i].Number==drag.transform.parent.GetComponent<Cell>().Number){
				numberItem=i;
				break;
			}
		}
		int NumNew = numberItem;									//запоминаем номер ячейки В которую положили

		if (ListItem[NumNew].NameRes==ListItem[NumOld].NameRes){	//проверяем одинаковость итемов при меретаскивании, если одинаковые будем стакать
			//print("!!");
			if (ListItem[NumNew].count+ListItem[NumOld].count<=ListItem[NumOld].MaxStaсk){	//можно полностью стакнуть
				ListItem[NumNew].count+=ListItem[NumOld].count;
				ListItem.Remove(ListItem[NumOld]);
				Draw.UpdateInventory(Panel,ListItem);
				drag.oldcanvas.GetComponent<Cell>().IsEmpty=true;			// убираем заполненость ячейки ИЗ КОТОРОЙ вытащили
				return;
			}
			else{													//можно стакнуть, но останется остаток	
				int rest=ListItem[NumNew].count+ListItem[NumOld].count-ListItem[NumOld].MaxStaсk;
				ListItem[NumNew].count=ListItem[NumOld].MaxStaсk;
				ListItem[NumOld].count=rest;
				Draw.UpdateInventory(Panel,ListItem);
				return;
			}
			
		}

		ListItem[NumOld].Number=drag.transform.parent.GetComponent<Cell>().Number;	//меняем номера местами
		ListItem[NumNew].Number=drag.oldcanvas.GetComponent<Cell>().Number; 

	}
	
	void dragInEmptyOther(PointerEventData eventData,List<Item> List1,List<Item> List2){				//таскаем в пустую ячейку МЕЖДУ инвентарями
		Item item;
		
		Drag drag =eventData.pointerDrag.GetComponent<Drag>();		//присваеваем драгу то что таскаем
		drag.transform.SetParent(transform);						//закидываем в новую ячейку
		drag.transform.parent.GetComponent<Cell>().IsEmpty=false;	// убираем заполненость ячейки ИЗ КОТОРОЙ вытащили
		drag.oldcanvas.GetComponent<Cell>().IsEmpty=true;			//Заполняем ячейку В КОТОРУЮ закинули
	
		for (int i=0;i<List1.Count;i++){								//ищем итем с номером ячейки ИЗ КОТОРОЙ перетащили
			if (List1[i].Number==drag.oldcanvas.GetComponent<Cell>().Number){
				numberItem=i;
				break;
			}
		}
		//int OldNumber=numberItem;	//запоминаем из какой ячейки вытащили, потом удалим
		item=List1[numberItem];	//берём нужный айтем
		List2.Add(item);		//добавляем айтем
		
		List2[List2.Count-1].Number=drag.transform.parent.GetComponent<Cell>().Number;	//присваеваем переменной "номер" элемента лист, номер ячейки куда мы закинули	
		List1.Remove(item);																//удаляем айтем инвентаря ИЗ КОТОРОГО перетасвивали
	}
	
	void dragInNotEmptyOther(PointerEventData eventData,List<Item> List1,List<Item> List2,GameObject Panel1, GameObject Panel2){
//перетаскиваем в заполненую ячейку из инвентаря в сундук, или наоборот лист1 и панель1 ИЗ ЧЕГО берём, лист2 и панель2 КУДА кладём
		Drag drag =eventData.pointerDrag.GetComponent<Drag>();		//присваеваем драгу то что таскаем
		drag.transform.SetParent(transform);						//закидываем в новую ячейку
		transform.GetChild(0).SetParent(drag.oldcanvas);			//перетаскиваем предмет из ячейки в которую закинули, в старую ячейку
		drag.transform.parent.GetComponent<Cell>().IsEmpty=false;	//заполняем ячейки ИЗ КОТОРОЙ вытащили
		drag.oldcanvas.GetComponent<Cell>().IsEmpty=false;			//Заполняем ячейку В КОТОРУЮ закинули

		for (int i=0;i<List1.Count;i++){							//ищем итем с номером ячейки ИЗ КОТОРОЙ перетащили
			if (List1[i].Number==drag.oldcanvas.GetComponent<Cell>().Number){
				numberItem=i;
				break;
			}
		}
		int NumOld=numberItem;										//запоминаем номер ячейки из которой взяли


		for (int i=0;i<List2.Count;i++){							//ищем итем с номером ячейки В КОТОРУЮ перетащили
			if (List2[i].Number==drag.transform.parent.GetComponent<Cell>().Number){
				numberItem=i;
				break;
			}
		}
		int NumNew = numberItem;									//запоминаем номер ячейки В которую положили

		if (List2[NumNew].NameRes==List1[NumOld].NameRes & List2[NumNew].count!=List2[NumNew].MaxStaсk){	//проверяем одинаковость итемов при меретаскивании, если одинаковые будем стакать
			if (List2[NumNew].count+List1[NumOld].count<=List1[NumOld].MaxStaсk){	//можно полностью стакнуть
				
				List2[NumNew].count+=List1[NumOld].count;
				List1.Remove(List1[NumOld]);								//Удаляем итем из 1го листа
				drag.oldcanvas.GetComponent<Cell>().IsEmpty=true;			// убираем заполненость ячейки ИЗ КОТОРОЙ вытащили
				Draw.UpdateInventory(Panel2,List2);
				Destroy(drag.oldcanvas.GetComponent<Cell>().transform.GetChild(0).gameObject);	//удаляем ячейку которую таскали
;
				return;
				
			}
			else{													//можно стакнуть, но останется остаток	
				int rest=List2[NumNew].count+List1[NumOld].count-List1[NumOld].MaxStaсk;
				List2[NumNew].count=List1[NumOld].MaxStaсk;
				List1[NumOld].count=rest;
				Drop drop=gameObject.GetComponent<Drop>();
				drag.oldcanvas.GetComponent<Cell>().transform.GetChild(0).GetChild(0).GetComponent<Text>().text=rest.ToString();	//Присваеваем из которой таскали остаток
				drop.transform.GetChild(0).GetChild(0).GetComponent<Text>().text=List1[NumOld].MaxStaсk.ToString();					//куда тащили, там максимальный стак

				return;
			}	
		}


		Item itemOld;
		itemOld=List1[NumOld];										//Сохраняем старый айтем
		List1.Remove(itemOld);										//удаляем айтем инвентаря ИЗ КОТОРОГО перетасвивали
		List1.Add(List2[NumNew]);									//кидаем в первый айтем ячейки в которую кинули
		List1[List1.Count-1].Number=drag.oldcanvas.GetComponent<Cell>().Number;							//присваевам номер айтема


		print("Сюда нельзя");
		Item ItemNew=List2[NumNew];
		List2.Remove(ItemNew);
		List2.Add(itemOld);											//добавляем старый айтем в новый инвентарь
		List2[List2.Count-1].Number=drag.transform.parent.GetComponent<Cell>().Number;	//присваеваем переменной "номер" элемента лист, номер ячейки куда мы закинули



		
	}
//drag.transform.parent.GetComponent<Cell>().Number; // номер ячейки В КОТОРУЮ закинул
//drag.oldcanvas.GetComponent<Cell>().Number; 		//Номер ячейки ИЗ КОТОРОЙ взяли

}
