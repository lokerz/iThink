using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loader : MonoBehaviour {
	private int index;
	private int n;

	private Image picBox;
	private Text textBox;
	private AudioSource musicBox;

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
		int x = (StageManager.loadStageID - 1) * 10; 
		for (int k = 0; k < i; k++) 
			pic.Add(Resources.Load("soal_"+(x+k+1).ToString(), typeof (Sprite))as Sprite);
		for(int k = 0; k <= j; k++)
			music.Add(Resources.Load((k).ToString(), typeof (AudioClip))as AudioClip);
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

		picBox = GameObject.Find ("ImageBox").GetComponent<Image> ();
		picBox.sprite = pic [index];

		musicBox = GameObject.Find ("MusicBox").GetComponent<AudioSource> ();
		musicBox.clip = music [ManagerRef.GetComponent<DatabaseManager2> ().musicid [index]];
		musicBox.Play();

		if (picBox.sprite == null) {
			picBox.color += new Color (0, 0, 0, -255);
			textBox = GameObject.Find ("QuestionBox").GetComponent<Text> ();
			textBox.text = ManagerRef.GetComponent<DatabaseManager2> ().questions [index];
		}

	
	}

}
