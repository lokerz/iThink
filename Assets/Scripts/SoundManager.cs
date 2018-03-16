using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour {
	// Use this for initialization
	private AudioSource musicBox;
	public Sprite soundOn, soundOff;

	void Start () {
		musicBox = GameObject.Find ("MusicBox").GetComponent<AudioSource>();
		musicBox.mute = StageManager.isMute;
	}
	
	// Update is called once per frame
	void Update () {
		if (!musicBox.mute)
			gameObject.GetComponentInChildren<Image> ().sprite = soundOn;
		else
			gameObject.GetComponentInChildren<Image> ().sprite = soundOff;
	}

	public void setVolume(){
		if (!musicBox.mute) {
			musicBox.mute = true;
			StageManager.isMute = true;
		} else {
			musicBox.mute = false;
			StageManager.isMute = false;
		}
	}

}
