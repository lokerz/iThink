using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Answer2 : MonoBehaviour {
	private string answerKey;
	private string answerPlayer;
	public int score;
	private int index;
	public int plusTime;
	public int plus;
	public int minus;
	public bool answerCatching = true;
	private GameObject ManagerRef;
	private GameObject TimerRef;

	// Use this for initialization
	void Start () {
		score = 0;
		ManagerRef = GameObject.Find ("GameManager");
		TimerRef = GameObject.Find ("Timer");

	}

	void FixedUpdate(){
		GameObject.Find ("Scoreboard").GetComponent<Text> ().text = score.ToString();
	}

	public void answerCheck(int index2){
		index = TimerRef.GetComponent<Timer2> ().id [TimerRef.GetComponent<Timer2> ().loopIndex];

		if (answerCatching) {
			if (ManagerRef.GetComponent<DatabaseManager3> ().answerKey [index] [index2] == '1') {
				TimerRef.GetComponent<Timer2> ().timeLeft += plusTime;
				score += plus;
			} else
				score -= minus;
		}
		
	}
}
