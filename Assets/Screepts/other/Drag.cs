using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler,IPointerClickHandler {

	public Transform canvas;
	public 	Transform oldcanvas;
	


	void Start()
	{

		//LoadList();
	}

	public void OnBeginDrag(PointerEventData eventData){
		oldcanvas=transform.parent;
		transform.SetParent(canvas);
		GetComponent<CanvasGroup>().blocksRaycasts = false;
	 }

	  
	public void OnDrag(PointerEventData eventData){
		transform.position=Input.mousePosition;
	  }

	  
	public void OnEndDrag(PointerEventData eventData){
		GetComponent<CanvasGroup>().blocksRaycasts = true;
		if (transform.parent==canvas){
			transform.SetParent(oldcanvas);
		}
	  }

	public void OnPointerClick(PointerEventData eventData){

	}
	 
}
