using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Checker2 : MonoBehaviour {
	private int index;
	private List<int> answerIndex;
	public int index2;
	public Sprite trueButton;
	public Sprite falseButton;


	public void check () {
		index = GameObject.Find("Timer").GetComponent<Timer2> ().id [GameObject.Find("Timer").GetComponent<Timer2> ().loopIndex];

		if (GameObject.Find ("GameManager").GetComponent<DatabaseManager3> ().answerKey [index] [index2] == '1') {
			gameObject.GetComponent<Image> ().sprite = trueButton;
		} else {
			gameObject.GetComponent<Image> ().sprite = falseButton;
		}
	}

}
