using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

	public int duration1;
	public int duration2;

	public GameObject canvas1;
	public GameObject canvas2;

	private int temp;
	private int n;

	// Use this for initialization
	void Start () {
		n = duration1;
		temp = duration1;

		StartCoroutine ("Countdown");
	}
	
	// Update is called once per frame
	void Update () {
		
		if (n <= 0) {
			StopCoroutine ("Countdown");
			if (temp == duration1) {
				n = duration2;
				temp = duration2;
				canvas1.SetActive (false);
				canvas2.SetActive (true);
				Debug.Log ("Start Countdown from "+ n);
				StartCoroutine ("Countdown");
			} else if (temp == duration2) {
				n = duration1;
				temp = duration1;
				canvas1.SetActive (true);
				canvas2.SetActive (false);
				Debug.Log ("Start Countdown from "+ n);
				StartCoroutine ("Countdown");
			}
				
		}
	}

	IEnumerator Countdown(){
		while (true) {
			Debug.Log (n);
			yield return new WaitForSeconds(1);
			n--;
		}
	}

}
