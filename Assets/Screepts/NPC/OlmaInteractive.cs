using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OlmaInteractive : MonoBehaviour {
	public GameObject OlmaPortret;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.E)){
			if (gameObject.transform.Find("Triger").GetComponent<Triger>().CanUse){		//проверяем активна ли метка взаимодействия на тригире Ольмы
				if (OlmaPortret.activeSelf){
					//DialogClose();
				}
				else {
					DialogOpen();
					if (PlayerPrefs.GetInt ("WoodForOlma")==1){
						if (GameObject.FindWithTag("Player").GetComponent<Inventory>().CountRes("Wood")>=5 ){      //проверяем выполнили ли квест
           					PlayerPrefs.SetInt ("WoodForOlma", 2);
							gameObject.GetComponent<InstantiateDialog>().UpdateAnswers();
       			 		}
					}
				}
			}

		}
		//if (Input.GetKeyDown(KeyCode.T)){
			//OlmaSay("1\n2");
		//}
	}

	public void DialogOpen(){
		OlmaPortret.SetActive(true);
		gameObject.GetComponent<InstantiateDialog>().ShowDialogue=true;
		GameObject.FindWithTag("Player").GetComponent<move>().CanMove=false;
		
	}

	public void DialogClose(){
		OlmaPortret.SetActive(false);
		gameObject.GetComponent<InstantiateDialog>().ShowDialogue=false;
		GameObject.FindWithTag("Player").GetComponent<move>().CanMove=true;
	}
}
