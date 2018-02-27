using UnityEngine;
using System;
using System.IO;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using Mono.Data.SqliteClient; 
using UnityEngine.UI;


public class DatabaseManager2 : MonoBehaviour {

	public int maxStage;
	public int n;
	public int nScore;

	public List<int> id; 
	public List<int> musicid;
	public List<string> answerKey;
	public List<List<string>> answers;
	public List<string> questions;
	public List<int> score;

	private string conn;
	private string sqlQuery;
	private string filepath;
	private string newpath;
	private IDbConnection dbconn;
	private IDbCommand dbcmd;
	private IDataReader reader;
	private GameObject TimerRef;

	// Use this for initialization
	void Start () {
		TimerRef = GameObject.Find ("Timer");

		id = new List<int>();
		musicid = new List<int>();
		answerKey = new List<string>();
		answers = new List<List<string>>();
		questions = new List<string> ();
		score = new List<int>();

		//DB OPEN
		#if UNITY_STANDALONE_WINDOWS || UNITY_EDITOR
			
			Debug.Log("Unity Editor / Windows");
			GameObject.Find("Title").GetComponent<Text>().text+="\nWindows Ver.";
			newpath = Application.dataPath + "/StreamingAssets/datas.db";
			
			
		#elif UNITY_ANDROID
			GameObject.Find("Title").GetComponent<Text>().text+="\nAndroid Ver.";
			filepath = "jar:file://" + Application.dataPath + "!/assets/datas.db"; 
			newpath = Application.persistentDataPath + "/datas.db";
			if(!File.Exists(newpath))
			{
				Debug.Log("file doesnt exist" );
				// if it doesn't ->
				// open StreamingAssets directory and load the db -> 
				Debug.Log(filepath);
				WWW loadDB = new WWW(filepath);
				while(!loadDB.isDone) {}
				// then save to Application.persistentDataPath
				File.WriteAllBytes(newpath, loadDB.bytes);
			}
		#endif

		//DB CONN
		conn = "URI=file:" + newpath; //Path to database.
		dbconn = new SqliteConnection(conn);
		dbconn.Open(); //Open connection to the database.

		//DB READ

		dbcmd = dbconn.CreateCommand();
		sqlQuery = "SELECT ID, MusicID, AnswerKey, A1, A2, A3, A4, A5, A6, A7, A8, A9, A10, Question " + "FROM Quiz";
		dbcmd.CommandText = sqlQuery;
		reader = dbcmd.ExecuteReader();

		n = 0;
		while (reader.Read())
		{
			id.Add(reader.GetInt32 (0));
			musicid.Add(reader.GetInt32 (1));
			answerKey.Add(reader.GetString (2));
			answers.Add (new List<string> ());
			for (int i = 0; i < 10; i++) {
				answers[n].Add(reader.GetString (3 + i));
			}
			questions.Add(reader.GetString(13));
			n++;

		}
		//READER CLOSE
		reader.Close();
		reader = null;
		dbcmd.Dispose();
		dbcmd = null;

		dbcmd = dbconn.CreateCommand();
		dbcmd.CommandText = "SELECT Score FROM HiScore";
		reader = dbcmd.ExecuteReader();
		nScore = 0;
		while(reader.Read()){
			score.Add (reader.GetInt32 (0));
			nScore++;
		}
		sortScore ();

		//READER CLOSE
		reader.Close();
		reader = null;
		dbcmd.Dispose();
		dbcmd = null;

		//DB CLOSE
		dbconn.Close();
		dbconn = null;

		//1st RUN
		TimerRef.GetComponent<Timer> ().Randomizer (n);
		TimerRef.GetComponent<Loader> ().ResLoader (); //load all pic into game 
		TimerRef.GetComponent<Loader> ().QuestionLoader (0);//Open first question
		TimerRef.GetComponent<Loader> ().ImageLoader (0);//Open first pic
		TimerRef.GetComponent<Timer> ().StartCoroutine("Countdown");//start countdown

	}
	
	// Update is called once per frame
	void Update () {
	}

	public void sortScore(){
		int temp;
		for (int i = nScore-1; i > 0; i--) {
			for (int j = i - 1; j >= 0; j--) {
				if (score [i] > score [j]) {
					temp = score [i];
					score [i] = score [j];
					score [j] = temp;
				}
			}
		}
	}

	public void UpdateScoreDB(int x){
		//DB CONN
		conn = "URI=file:" + newpath; //Path to database.
		dbconn = new SqliteConnection(conn);
		dbconn.Open(); //Open connection to the database.

		//DB READ
		dbcmd = dbconn.CreateCommand();
		sqlQuery = "INSERT INTO HiScore (Score) VALUES ('"+x+"')";
		dbcmd.CommandText = sqlQuery;
		dbcmd.ExecuteNonQuery();
		sqlQuery = "DELETE FROM HiScore WHERE Score IN (SELECT Score FROM HiScore ORDER BY Score ASC LIMIT 1)";
		dbcmd.CommandText = sqlQuery;
		dbcmd.ExecuteNonQuery();

		dbcmd.Dispose();
		dbcmd = null;

		//DB CLOSE
		dbconn.Close();
		dbconn = null;
	}
}
