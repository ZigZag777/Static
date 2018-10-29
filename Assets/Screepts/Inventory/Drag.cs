using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler,IPointerClickHandler {

	public Transform canvas;
	public 	Transform oldcanvas;
	


	void Start()
	{

		canvas= GameObject.Find("Canvas").transform;
	}

	public void OnBeginDrag(PointerEventData eventData){
		oldcanvas=transform.parent;
		transform.SetParent(canvas);
		GetComponent<CanvasGroup>().blocksRaycasts = false;
		//print(gameObject.name);
	 }

	  
	public void OnDrag(PointerEventData eventData){
		//print(gameObject.name);
		//print(transform.position.x);
		transform.position=Input.mousePosition;
	  }

	  
	public void OnEndDrag(PointerEventData eventData){
		GetComponent<CanvasGroup>().blocksRaycasts = true;
		if (transform.parent==canvas){
			transform.SetParent(oldcanvas);
		}
	  }

	public void OnPointerClick(PointerEventData eventData){
		//if (eventData.button == PointerEventData.InputButton.Left){
		//	print(gameObject.name);
		//}


	}
	 
}
