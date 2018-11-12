using UnityEngine;
using System.Collections;
using System.Collections.Generic;
 
public class InstantiateDialog : MonoBehaviour
{
    public GameObject mus;
    public TextAsset ta;
    public Dialog dialog;
    public int currentNode;
    public bool ShowDialogue;
    private QuestHolder QH;
 
    public GUISkin skin;
 
    public List<Answer> answers = new List<Answer>();
 
    void Start ()
    {
        dialog = Dialog.Load (ta);
        skin = Resources.Load ("Fonts/Skin") as GUISkin;
        UpdateAnswers ();
        QH=GameObject.Find("_Holder").GetComponent<QuestHolder>();
    }
 
    void Update()
    {
        
       // print(currentNode);
       //UpdateAnswers ();
    }
 
    public void UpdateAnswers ()
    {

        answers.Clear ();
        for (int i = 0; i < dialog.nodes [currentNode].answers.Length; i++)
        {
            if(dialog.nodes [currentNode].answers [i].QuestName==null||dialog.nodes [currentNode].answers [i].NeedQuestValue==PlayerPrefs.GetInt(dialog.nodes [currentNode].answers [i].QuestName))
            answers.Add (dialog.nodes [currentNode].answers [i]);
        }
        
    }
 
    void OnGUI()
    {
        GUI.skin = skin;
        if (ShowDialogue) {
            //GUI.Box (new Rect (Screen.width / 2 - 300, Screen.height - 300, 600, 250), "");
            //GUI.Label (new Rect (Screen.width / 2 - 250, Screen.height - 280, 500, 90), dialog.nodes [currentNode].NpcText);

            GUI.Box (new Rect ( Screen.width / 2+200,  Screen.height -215, 270, 165), "Ольма");
            GUI.Box (new Rect (Screen.width / 2 - 400, Screen.height - 300, 600, 250), "");
            GUI.Label (new Rect (Screen.width / 2 - 350, Screen.height - 280, 500, 90), dialog.nodes [currentNode].NpcText);
            for (int i = 0; i < answers.Count; i++)
            {
                //if (GUI.Button (new Rect (Screen.width / 2 - 350, Screen.height - 200 + 25 * i, 500, 25), answers [i].text, skin.label))
                if (GUI.Button (new Rect (Screen.width / 2 - 350, Screen.height - 200 + 25 * i, 500, 25), answers [i].text))
                {
                    //print(GameObject.FindWithTag("Player").GetComponent<Inventory>().CountRes("Wood"));
                    if(answers[i].QuestValue>0){
                        bool fin =false;
                        //включаем музыку
                        mus.SetActive(true);
                        PlayerPrefs.SetInt (answers [i].QuestName, answers[i].QuestValue);
                        if (answers[i].Finished=="true"){
                            fin=true;
                                // засовываем специи в инвентарь
                              	Item item =new Item();	
			                    item.NameRes="Spice";
                            	item.sprite="icons/Spice";
                            	item.prefab="icons/Spice";
                            	item.type="icons/Spice";
			                    item.count=1;
			                    item.MaxStaсk=50;        
                                GameObject.FindWithTag("Player").GetComponent<Inventory>().PootToInventory(item);
                                //удаляем дрова
                                GameObject.FindWithTag("Player").GetComponent<Inventory>().RemoveFromInventory("Wood",5);
                        }
                        QH.ChangeQuest(answers[i].QuestName,answers[i].QuestValue,fin,answers[i].Description);
                    }
                    if (answers [i].end == "true")
                    {
                        ShowDialogue = false;
                        gameObject.GetComponent<OlmaInteractive>().DialogClose();
                    }
                    if (answers [i].RewardGold > 0)
                    {
                        //Character.Gold += answers [i].RewardGold;
                    }
                    if (answers[i].any){
                        GameObject.Find("_Holder").GetComponent<NPC>().OlmaGoParty=true;
                    }
                    currentNode = answers [i].nextNode;
                    UpdateAnswers ();
                }
            }
        }
    }
}

/*
    void OnGUI(){
        GUI.skin = skin;
        
        if (ShowDialogue) {
            GUI.Box (new Rect ( Screen.width / 2+200,  Screen.height -215, 270, 165), "Ольма");
            GUI.Box (new Rect (Screen.width / 2 - 400, Screen.height - 300, 600, 250), "");
            GUI.Label (new Rect (Screen.width / 2 - 350, Screen.height - 280, 500, 90), dialog.nodes [currentNode].NpcText);
            for (int i = 0; i < dialog.nodes [currentNode].answers.Length; i++) {
                if (GUI.Button (new Rect (Screen.width / 2 - 350, Screen.height - 200 + 25 * i, 500, 25), dialog.nodes [currentNode].answers [i].text)) {
                    if (dialog.nodes [currentNode].answers [i].end == "true")
                    {
                        ShowDialogue = false;
                        gameObject.GetComponent<OlmaInteractive>().DialogClose();
                    }
                    currentNode = dialog.nodes [currentNode].answers [i].nextNode;
                }
            }
        }
    }
*/