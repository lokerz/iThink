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
	public int nMusic;

	public List<int> id;
	public List<int> musicid;
	public List<string> answerKey;
	public List<List<string>> answers;
	public List<string> questions;
	public List<int> score;

	private int stage;
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
		stage = StageManager.loadStageID;
		if(stage == 0) stage = 1;

		
		//DB OPEN
		#if UNITY_STANDALONE_WIN || UNITY_EDITOR
			
			newpath = Application.dataPath + "/StreamingAssets/datas.db";
			
			
		#elif UNITY_ANDROID
			filepath = "jar:file://" + Application.dataPath + "!/assets/datas.db"; 
			newpath = Application.persistentDataPath + "/datas.db";
			if(!File.Exists(newpath))
			{
				WWW loadDB = new WWW(filepath);
				while(!loadDB.isDone) {}
				File.WriteAllBytes(newpath, loadDB.bytes);
			}
		#endif

		//DB CONN
		conn = "URI=file:" + newpath; //Path to database.
		dbconn = new SqliteConnection(conn);
		dbconn.Open(); //Open connection to the database.

		//DB READ

		dbcmd = dbconn.CreateCommand();
		sqlQuery = "SELECT ID, MusicID, AnswerKey, A1, A2, A3, A4, A5, A6, A7, A8, A9, A10, Question " + "FROM Quiz " + "WHERE StageID = " + stage;
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

		//DB CLOSE
		dbconn.Close();
		dbconn = null;

		//1st RUN
		MusicCount(musicid);
		TimerRef.GetComponent<Timer> ().Randomizer (n);
		TimerRef.GetComponent<Loader> ().ResLoader (n,nMusic); //load all res into game 
		TimerRef.GetComponent<Loader> ().QuestionLoader (0);//Open first question
		//TimerRef.GetComponent<Timer> ().StartCoroutine("Countdown");//start countdown
		Debug.Log(id.Count);

	}

	public void MusicCount(List<int> i){
		for (int j = 0; j < n-1; j++)
			if (musicid [j] > musicid [j + 1])
				nMusic = musicid [j];
	}


	public void UpdateScoreDB(int score){
		//DB CONN
		conn = "URI=file:" + newpath; //Path to database.
		dbconn = new SqliteConnection(conn);
		dbconn.Open(); //Open connection to the database.

		//DB READ
		dbcmd = dbconn.CreateCommand();
		sqlQuery = "UPDATE HiScore SET Score = CASE WHEN "+ score +" > (SELECT Score FROM HiScore WHERE StageID = "+ stage +") THEN "+ score +"  ELSE (SELECT Score FROM HiScore WHERE StageID = "+ stage +") END WHERE StageID = "+ stage;
		dbcmd.CommandText = sqlQuery;
		dbcmd.ExecuteNonQuery();
	
		dbcmd.Dispose();
		dbcmd = null;

		//DB CLOSE
		dbconn.Close();
		dbconn = null;
	}
}
