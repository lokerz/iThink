using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

	public GameObject pauseCanvas;
	public GameObject picCanvas;
	public GameObject answerCanvas;
	public GameObject optionCanvas;
	public GameObject mainCanvas;
	public bool isPaused = false;
	public bool isOption = false;
	// Use this for initialization

	void Start () {
		if(pauseCanvas!=null) pauseCanvas.SetActive (false);
		if(answerCanvas!=null) answerCanvas.SetActive (false);
		if(optionCanvas!=null) optionCanvas.SetActive (false);
		if(picCanvas!=null) picCanvas.SetActive (true);
		if(mainCanvas != null) mainCanvas.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {
		if (isPaused) paused ();
		else if (!isPaused) unPause ();

		if (isPaused && isOption) option ();
		else if (isPaused && !isOption) unOption ();

		if(Input.GetKeyDown(KeyCode.Escape)){
			isPaused = !isPaused;
			Debug.Log ("Pause");
		}

		if(isPaused && Input.GetKeyDown(KeyCode.O)){
			isOption = !isOption;
			Debug.Log ("Option");
		}
	}

	public void paused(){
		if(pauseCanvas!=null) pauseCanvas.SetActive (true);
		Time.timeScale = 0f;
	}

	public void unPause(){
		if(pauseCanvas!=null) pauseCanvas.SetActive (false);
		Time.timeScale = 1f;
	}

	public void option(){
		if(pauseCanvas!=null) pauseCanvas.SetActive (false);
		if(optionCanvas!=null) optionCanvas.SetActive (true);
	}

	public void unOption(){
		if(optionCanvas!=null) optionCanvas.SetActive (false);
		if(pauseCanvas!=null) pauseCanvas.SetActive (true);
	}


}
