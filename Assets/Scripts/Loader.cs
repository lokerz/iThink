using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loader : MonoBehaviour {
	//private int index;
	public Image picbox;
	public List<Sprite> pic = new List<Sprite>();
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ResLoader(){
		int limit = GameObject.Find ("GameManager").GetComponent<DatabaseManager2> ().n;
		int temp;
		//int limit = 25;
		for (int i = 0; i < limit; i++) {
			temp = i + 1;
			pic.Add(Resources.Load(temp.ToString(), typeof (Sprite))as Sprite);
		}
	}
	public void ImageLoader(int i){
		//index = GameObject.Find ("Randomizer").GetComponent<Randomizer> ().index [i];
		//index = 0;
		picbox = GameObject.Find ("ImageBox").GetComponent<Image> ();
		picbox.sprite = pic [i];
	
	}

	public void AnswerLoader(int i){
	
	}
}
