using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleControl : MonoBehaviour {

	public int[] toggleCount;
	public int toggleCounter;
	// Use this for initialization
	void Start () {
		toggleCount = new int[10];
		toggleCounter = 0;
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < 10; i++) {
			if (GameObject.Find ("Toggle" + i).GetComponent<Toggle> ().isOn) {
				toggleCount [i] = 1;
				GameObject.Find ("Toggle" + i).GetComponent<Toggle> ().interactable = false;
			}
			else
				toggleCount [i] = 0;
			toggleCounter += toggleCount [i];
		}

		toggleCounter =  toggleCount [0]+ toggleCount [1]+ toggleCount [2]+ toggleCount [3]+ toggleCount [4]+ toggleCount [5]+ toggleCount [6]+ toggleCount [7]+ toggleCount [8]+ toggleCount [9];


		if (toggleCounter >= 5) {
			for (int i = 0; i < 10; i++) {
				if (!GameObject.Find ("Toggle" + i).GetComponent<Toggle> ().isOn)
					GameObject.Find ("Toggle" + i).GetComponent<Toggle> ().interactable = false;
			}

		} /*else {
			for (int i = 0; i < 10; i++) {
				if (!GameObject.Find ("Toggle"+i).GetComponent<Toggle> ().interactable)
					GameObject.Find ("Toggle"+i).GetComponent<Toggle> ().interactable = true;
			}
		}*/
	
	}

}
