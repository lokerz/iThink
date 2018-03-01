using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour {

	public GameObject pauseCanvas;
	public GameObject picCanvas;
	public GameObject answerCanvas;
	public GameObject optionCanvas;
	public GameObject mainCanvas;
	public GameObject overCanvas;
	public GameObject stageCanvas;

	public bool isPaused = false;
	public bool isOption = false;
	public bool isOver = false;
	public bool isStage = false;
	public string load;
	// Use this for initialization

	void Start () {
		if(pauseCanvas!=null) pauseCanvas.SetActive (false);
		if(answerCanvas!=null) answerCanvas.SetActive (false);
		if(optionCanvas!=null) optionCanvas.SetActive (false);
		if(picCanvas!=null) picCanvas.SetActive (true);
		if(mainCanvas != null) mainCanvas.SetActive (true);
		if(overCanvas != null) overCanvas.SetActive (false);
		if(stageCanvas != null) stageCanvas.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (isPaused) paused ();
		else if (!isPaused) unPause ();

		if (isPaused && isOption) option ();
		else if (isPaused && !isOption) unOption ();

		if(Input.GetKeyDown(KeyCode.Escape) && !isOver){
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

	public void GameOver(){
		if(answerCanvas!=null) answerCanvas.SetActive (false);
		if(picCanvas!=null) picCanvas.SetActive (false);
		if(overCanvas!=null) overCanvas.SetActive (true);
		isOver = true;
	}

	public void SelectStage(){
		if (stageCanvas != null && !isStage) {
			stageCanvas.SetActive (true);
			isStage = true;
			gameObject.GetComponent<DatabaseManager> ().HiScorePost ();
		} else if (stageCanvas != null && isStage) {
			stageCanvas.SetActive (false);
			isStage = false;
		}
	}

	public void Retry(){
		SceneManager.LoadScene ("gameplay");
	}

	public void Load(){
		SceneManager.LoadScene (load);
	}
}
