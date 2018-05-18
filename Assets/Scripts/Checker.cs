using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Checker : MonoBehaviour {
	private int index;
	public int index2;
	public Sprite trueButton;
	public Sprite falseButton;
	// Use this for initialization
	public void check () {
		index = GameObject.Find("Timer").GetComponent<Timer> ().id [GameObject.Find("Timer").GetComponent<Timer> ().loopIndex];

		if(GameObject.Find("GameManager").GetComponent<DatabaseManager2>().answerKey[index][index2] == '1')
			gameObject.GetComponent<Image> ().sprite = trueButton;
		else
			gameObject.GetComponent<Image> ().sprite = falseButton;
	}

}
