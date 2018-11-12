using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestHolder : MonoBehaviour {
	public	List<Quest> QeustList; 
	

	// Use this for initialization
	void Start () {
		QeustList = new List<Quest>(); //создаём массив итемов
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ChangeQuest(string NameQuest, int QuestValue, bool Finished,string description){
		bool CreateNew=true;
		for (int i=0;i<QeustList.Count;i++){
			if(QeustList[i].NameQuest==NameQuest){
				QeustList[i].QeustValue=QuestValue;
				QeustList[i].Finished=Finished;
				QeustList[i].description=description;
				GameObject.Find("QuestDescrip").GetComponent<Text>().text = description;
				CreateNew=false;
			}
		}
		if (CreateNew){
			//создаём новый
			Quest quest=new Quest();
			quest.NameQuest=NameQuest;
			quest.QeustValue=QuestValue;
			quest.Finished=Finished;
			quest.description=description;
			QeustList.Add(quest);
			GameObject.Find("QuestDescrip").GetComponent<Text>().text = description;
		}
	}
}
