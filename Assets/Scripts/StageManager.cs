using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour {

	public static int loadStageID;
	public static bool isMute = false;

	public void setStage(int i){
		loadStageID = i;
	}
}
