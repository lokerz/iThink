using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour {

	public static int loadStageID;
	public static bool isMute = false;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setStage(int i){
		loadStageID = i;
	}
}
