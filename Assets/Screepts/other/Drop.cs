using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drop : MonoBehaviour,IDropHandler {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnDrop(PointerEventData eventData){
		Drag drag =eventData.pointerDrag.GetComponent<Drag>();
		if (drag!=null){
			if (transform.childCount>0){
				transform.GetChild(0).SetParent(drag.oldcanvas);
			}
			drag.transform.SetParent(transform);
		}
	}
}
