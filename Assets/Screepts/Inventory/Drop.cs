using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drop : MonoBehaviour,IDropHandler     {                             //,IPointerClickHandler {

	
	private int numberItem;
	public	List<Item> list; 


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}



	public void OnDrop(PointerEventData eventData){
	//print(list[numberItem].Number);
	}
	

	void dragInNotEmpty(PointerEventData eventData) {

	}


//drag.transform.parent.GetComponent<Cell>().Number; // номер ячейки В КОТОРУЮ закинул
//drag.oldcanvas.GetComponent<Cell>().Number; 		//Номер ячейки ИЗ КОТОРОЙ взяли

}
