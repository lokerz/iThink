using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {


	public List<int> id;
	public int questionTime;
	public int answerTime;

	public GameObject canvas1;
	public GameObject canvas2;

	private int timeTemp;
	private int timeLeft;
	private int loopIndex;

	private GameObject ManagerRef;
	private GameObject AnswerRef;
	private bool isAnswer = false;

	void Start () {
		ManagerRef = GameObject.Find ("GameManager");
		AnswerRef = GameObject.Find ("AnswerCatcher");

		timeLeft = questionTime;
		timeTemp = questionTime;
		loopIndex = 0;

	}
	
	// Update is called once per frame
	void Update () {
		
		if (timeLeft <= 0  && loopIndex < ManagerRef.GetComponent<DatabaseManager2>().n) {
			StopCoroutine ("Countdown");
			if (isAnswer) { //catch answer
				AnswerRef.GetComponent<Answer> ().answerPlayerCatch ();
				ResetToggle (); //Release all toggles to off
				isAnswer = false;
			}

			if (timeTemp == questionTime) {
				//start answer canvas
				isAnswer = true;
				timeLeft = answerTime;
				timeTemp = answerTime;
				canvas1.SetActive (false);
				canvas2.SetActive (true);
				Debug.Log ("Start Countdown from "+ timeLeft);
				gameObject.GetComponent<Loader> ().AnswerLoader (loopIndex);
				StartCoroutine ("Countdown");

			} else if (timeTemp == answerTime) {
				loopIndex++;

				if (loopIndex == ManagerRef.GetComponent<DatabaseManager2> ().n) {
					ManagerRef.GetComponent<Manager> ().GameOver ();
					AnswerRef.GetComponent<Answer> ().answerKeyCompile ();
					AnswerRef.GetComponent<Answer> ().calculateScore ();

				}
				else {
					//start pic canvas
					timeLeft = questionTime;
					timeTemp = questionTime;
					canvas1.SetActive (true);
					canvas2.SetActive (false);
					Debug.Log ("Start Countdown from " + timeLeft);
					gameObject.GetComponent<Loader> ().ImageLoader (loopIndex);
					gameObject.GetComponent<Loader> ().QuestionLoader (loopIndex);
					StartCoroutine ("Countdown");
				}
			}
		}
	}

	IEnumerator Countdown(){
		while (true) {
			GameObject.Find ("TimerBox").GetComponent<Text> ().text = timeLeft.ToString();
			yield return new WaitForSeconds(1);
			timeLeft--;
		}
	}
	
	public void Randomizer(int i){
		id = new List<int>();
		for (int j = 0; j < i; j++){
			id.Add (j);
		}

		for (int j = 0; j < id.Count; j++) //bagian ngeshuffle
		{
			int temp = id[j];		
			int randomIndex = Random.Range(j, id.Count);
			id[j] = id[randomIndex];
			id[randomIndex] = temp;
			//Debug.Log ("Indeks ke " + j + " adalah " + id [j]); //buat ngecek hasil shuffle
		}
	}
		
	public void ResetToggle(){
		for (int i = 0; i < 10; i++) {
			GameObject.Find ("Toggle" + i).GetComponent<Toggle> ().isOn = false;
		}
		GameObject.Find ("Answers").GetComponent<ToggleControl> ().toggleCounter = 0;
	}

}
