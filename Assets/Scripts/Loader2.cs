using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loader2 : MonoBehaviour {
	private int index;
	private int n;

	private Image picBox;
	private Text textBox;
	//private AudioSource musicBox;

	private List<Sprite> pic;
	private List<AudioClip> music;
	private List<Text> answer;

	private GameObject ManagerRef;

	// Use this for initialization
	void Start () {
		pic = new List<Sprite>();
		answer = new List<Text> ();
		music = new List<AudioClip> ();
		ManagerRef = GameObject.Find ("GameManager");
	}


	public void ResLoader(int i, int j){
		for (int k = 0; k < i; k++) 
			pic.Add(Resources.Load("soal_"+(k+1).ToString(), typeof (Sprite))as Sprite);
		for(int k = 0; k <= j; k++)
			music.Add(Resources.Load((k).ToString(), typeof (AudioClip))as AudioClip);
	}
		
	public void AnswerLoader(int i){
		index = gameObject.GetComponent<Timer2> ().id [i];
		for (int j = 0; j < 10; j++) {
			answer.Add(GameObject.Find("Answer"+j).GetComponent<Text>());
			answer [j].GetComponentInChildren<Text> ().text = ManagerRef.GetComponent<DatabaseManager3> ().answers [index] [j];
			GameObject.Find ("Circle" + j).GetComponent<Checker2> ().check ();
		}
	}

	public void QuestionLoader(int i){
		index = gameObject.GetComponent<Timer2> ().id [i];

		picBox = GameObject.Find ("ImageBox").GetComponent<Image> ();
		picBox.sprite = pic [index];

		if (picBox.sprite == null) {
			picBox.color += new Color (0, 0, 0, -255);
			textBox = GameObject.Find ("QuestionBox").GetComponent<Text> ();
			textBox.text = ManagerRef.GetComponent<DatabaseManager3> ().questions [index];
		}

	
	}

}
