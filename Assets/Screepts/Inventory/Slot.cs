using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour,IPointerClickHandler {

	public Item item;
	public int Number;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

		public void OnPointerClick(PointerEventData eventData){
		if (eventData.button == PointerEventData.InputButton.Left){
		//	print(item.count);
		//	print(Number);
		}


	}
}
