using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Answer : MonoBehaviour {
	//public int[] answer = new int[10]{0,0,0,0,0,0,0,0,0,0};
	private string answerKey;
	private string answerPlayer;
	private int score;
	public int plus;
	public int minus;

	private GameObject ManagerRef;
	private GameObject TimerRef;

	// Use this for initialization
	void Start () {
		score = 0;
		ManagerRef = GameObject.Find ("GameManager");
		TimerRef = GameObject.Find ("Timer");

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void answerKeyCompile(){
		for (int i = 0; i < ManagerRef.GetComponent<DatabaseManager2> ().n; i++) {
			answerKey += ManagerRef.GetComponent<DatabaseManager2>().answerKey[TimerRef.GetComponent<Timer> ().id [i]];
		}
		Debug.Log (answerKey);
	}

	public void answerPlayerCatch(){
		for (int i = 0; i < 10; i++) {
			if (GameObject.Find ("Toggle" + i).GetComponent<Toggle> ().isOn)
				answerPlayer += "1";
			else
				answerPlayer += "0";
		}
		Debug.LogWarning (answerPlayer);
	}
		
	public void calculateScore(){
		for (int i = 0; i < ((10*ManagerRef.GetComponent<DatabaseManager2> ().n)); i++) {
			if (answerPlayer [i] == '1' && answerKey [i] == '1')
				score += plus;
			else if (answerPlayer [i] == '1' && answerKey [i] == '0')
				score -= minus;
			else if (answerPlayer [i] == '0' && answerKey [i] == '1')
				score -= minus;
			else
				score += 0;
		}
		GameObject.Find ("Scoreboard").GetComponent<Text> ().text = score + "/" + (10 * 5 * ManagerRef.GetComponent<DatabaseManager2> ().n);
	}

}
