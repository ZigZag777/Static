using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class proba : MonoBehaviour {

public GameObject Panel;
	// Use this for initialization
	public Usable inuse;
	public int asdds;
	public GameObject mus;
	void Start () {
		inuse=GameObject.Find("treeInterface").GetComponent<UsingTree>();
	}
	
	// Update is called once per frame

	int a (){
		return 5;
	}
	void Update () {
		
		if (Input.GetKeyDown (KeyCode.T)) {
			//inuse.use();
		}
		
		

		if (Input.GetKeyDown (KeyCode.R)) {
			PlayerPrefs.DeleteAll();
		}
	
		if (Input.GetKeyDown (KeyCode.Z)) {
			
			Item item =new Item();	
			item.NameRes="Spice";
			item.sprite="icons/Spice";
			item.prefab="icons/Spice";
			item.type="icons/Spice";
			item.count=1;
			item.MaxStaсk=50;
			gameObject.GetComponent<Inventory>().PootToInventory(item);
			print(item); // проверяю

		}

		if (Input.GetKeyDown (KeyCode.X)) {
			//gameObject.GetComponent<Inventory>().RemoveFromInventory("Meat",15);
			gameObject.GetComponent<Inventory>().RemoveFromInventory("Wood",5);
		}
		if (Input.GetKeyDown (KeyCode.M)) {
			mus.SetActive(true);
		}
		
	}

	

	public void b (){
		print (a());
	}
}
