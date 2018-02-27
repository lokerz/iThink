using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loader : MonoBehaviour {
	private int index;
	private int n;

	private Image picbox;
	private Text textBox;

	private List<Sprite> pic;
	private List<Text> answer;

	private GameObject ManagerRef;

	// Use this for initialization
	void Start () {
		pic = new List<Sprite>();
		answer = new List<Text> ();
		ManagerRef = GameObject.Find ("GameManager");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ResLoader(){
		//n = ManagerRef.GetComponent<DatabaseManager2> ().n;
		for (int i = 0; i < ManagerRef.GetComponent<DatabaseManager2> ().n; i++) {
			pic.Add(Resources.Load((i+1).ToString(), typeof (Sprite))as Sprite);
		}
	}

	public void ImageLoader(int i){
		index = gameObject.GetComponent<Timer> ().id [i];
		picbox = GameObject.Find ("ImageBox").GetComponent<Image> ();
		picbox.sprite = pic [index];
	}

	public void AnswerLoader(int i){
		index = gameObject.GetComponent<Timer> ().id [i];

		for (int j = 0; j < 10; j++) {
			answer.Add(GameObject.Find("Answer"+j).GetComponent<Text>());
			answer [j].GetComponentInChildren<Text> ().text = ManagerRef.GetComponent<DatabaseManager2> ().answers [index] [j];
		}
	}

	public void QuestionLoader(int i){
		index = gameObject.GetComponent<Timer> ().id [i];
		textBox = GameObject.Find ("QuestionBox").GetComponent<Text> ();
		textBox.text = ManagerRef.GetComponent<DatabaseManager2> ().questions [index];
	}

}
