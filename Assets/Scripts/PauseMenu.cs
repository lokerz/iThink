using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
	
	public string menu;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Resume(){
		GameObject.Find("GameManager").GetComponent<Manager>().isPaused = false;
	}

	public void Option(){
		GameObject.Find ("GameManager").GetComponent<Manager> ().isOption = true;
		GameObject.Find ("GameManager").GetComponent<Manager>().isPaused = false;
	}

	public void MainMenu(){
		if(menu != null)
			SceneManager.LoadScene (menu);
	}
		
}
