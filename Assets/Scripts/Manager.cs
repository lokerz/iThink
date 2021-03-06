using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour {

	public GameObject pauseCanvas;
	public GameObject picCanvas;
	public GameObject answerCanvas;
	public GameObject mainCanvas;
	public GameObject overCanvas;
	public GameObject stageCanvas;
	public GameObject helpCanvas;

	public bool isPaused = false;
	public bool isOption = false;
	public bool isOver = false;
	public bool isStage = false;
	public bool isHelp = false;

	// Use this for initialization

	void Start () {
		if(pauseCanvas!=null) pauseCanvas.SetActive (false);
		if(answerCanvas!=null) answerCanvas.SetActive (false);
		if(picCanvas!=null) picCanvas.SetActive (true);
		if(mainCanvas != null) mainCanvas.SetActive (true);
		if(overCanvas != null) overCanvas.SetActive (false);
		if(stageCanvas != null) stageCanvas.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (isPaused) pause ();
		else if (!isPaused) unPause ();

		if(Input.GetKeyDown(KeyCode.Escape) && !isOver){
			isPaused = !isPaused;
		}
			
	}

	public void pause(){
		isPaused = true;
		if(pauseCanvas!=null) pauseCanvas.SetActive (true);
		Time.timeScale = 0f;
	}

	public void unPause(){
		isPaused = false;
		if(pauseCanvas!=null) pauseCanvas.SetActive (false);
		Time.timeScale = 1f;
	}
		

	public void GameOver(){
		if(answerCanvas!=null) answerCanvas.SetActive (false);
		if(picCanvas!=null) picCanvas.SetActive (false);
		if(overCanvas!=null) overCanvas.SetActive (true);
		if (StageManager.loadStageID == 5 || StageManager.loadStageID == 6)
			GameObject.Find ("NextStageButton").SetActive (false);
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

	public void Help(){
		if (helpCanvas != null && !isHelp) {
			helpCanvas.SetActive (true);
			isHelp = true;
		} else if (helpCanvas != null && isHelp) {
			helpCanvas.SetActive (false);
			isHelp = false;
		}
	}

	public void LoadStage (string load){
		SceneManager.LoadScene (load);
	}

	public void Retry(){
		if (StageManager.loadStageID == 6)
			LoadStage ("gameplay2");
		else
			LoadStage ("gameplay");

	}

	public void MainMenu(){
		LoadStage("main");
	}
		
	public void NextStage(){
		StageManager.loadStageID ++;
		LoadStage("gameplay");
	}

	public void Exit(){
		Application.Quit ();
	}

	public void Vibrate(){
		#if UNITY_ANDROID
		Handheld.Vibrate ();
		#endif
	}
}
