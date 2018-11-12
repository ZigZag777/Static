using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsingTree : MonoBehaviour, Usable {
	private float HPWood=30;
	private Character CharPlayer;
	public Transform Wood;

	// Use this for initialization
	void Start () {
		CharPlayer=GameObject.FindWithTag("Player").GetComponent<Character>();
		//print(CharPlayer.AxeSpeed);
		//print(GameObject.FindWithTag("Player").GetComponent<Character>().AxeSpeed);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void use(){
		
		HPWood-=CharPlayer.AxeSpeed;
		print("Рубим дерево");
		float wx=transform.position.x+Random.Range(-1f,1f);
		float wy=transform.position.y+Random.Range(-1f,1f);
		//print(Random.Range());
		if(HPWood<=0){
			Instantiate(Wood, new Vector3(wx, wy, transform.position.z), Quaternion.identity);
			GameObject.FindWithTag("Player").GetComponent<Interactions>().OtherObj=null;
			Destroy(this.gameObject);
			print("Слубили");
		}
		//print(CharPlayer.AxeSpeed);
		
	}
	
}
