using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour {

	public static int loadStageID;
	// Use this for initialization
	void Start () {
		loadStageID = 1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setStage(int i){
		loadStageID = i;
	}
}
