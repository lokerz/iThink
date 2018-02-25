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

	public int[] id = new int[25];
	public int[] musicid = new int[25];
	public string[] answerKey = new string[25];
	public string[,] answers = new string[25,10];

	private string conn;
	private string sqlQuery;
	private IDbConnection dbconn;
	private IDbCommand dbcmd;
	private IDataReader reader;
	private StringBuilder builder;

	// Use this for initialization
	void Start () {
		//DB OPEN
		Debug.Log("starting SQLiteLoad app");
		//string filepath = "jar:file://" + Application.dataPath + "!/assets/datas.db"; // System.IO.Path.Combine(Application.streamingAssetsPath, "datas.s3db");
		//string newpath = Application.persistentDataPath + "/datas.db";
		string newpath = Application.dataPath + "/StreamingAssets/datas.db";
		/*if(!File.Exists(newpath))
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
		else 
			Debug.Log("file doesnt exist" );*/
		conn = "URI=file:" + newpath; //Path to database.
		Debug.Log(newpath);
		dbconn = new SqliteConnection(conn);
		dbconn.Open(); //Open connection to the database.

		//DB READ
		dbcmd = dbconn.CreateCommand();
		sqlQuery = "SELECT ID, MusicID, AnswerKey, A1, A2, A3, A4, A5, A6, A7, A8, A9, A10 " + "FROM Quiz";
		dbcmd.CommandText = sqlQuery;
		reader = dbcmd.ExecuteReader();
		int n = 0;
		while (reader.Read())
		{
			id [n] = reader.GetInt32 (0);
			musicid [n] = reader.GetInt32 (1);
			answerKey [n] = reader.GetString (2);
			Debug.Log (n+" "+ id [n] + " " + answerKey [n] + " ");
			for (int i = 0; i < 10; i++) {
				answers [n, i] = reader.GetString (3 + i);
				Debug.Log (n + "," + i + " " + answers [n, i] + " ");
			}
			n++;
		}

		//DB CLOSE
		reader.Close();
		reader = null;
		dbcmd.Dispose();
		dbcmd = null;
		dbconn.Close();
		dbconn = null;
		/*
		for(int i = 0; i< 25; i++){
			Debug.Log (id [i] + " " + answerKey [i] + " ");
			for (int j = 0; j < 10; j++)
				Debug.Log (i+","+j+" "+answers [i, j] + " ");
		}*/
		dataWrite ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void dataWrite(){
		Text box = GameObject.Find ("TextBox").GetComponent<Text>();
		box.text = "Answers : ";
		for (int i = 0; i < 25; i++) {
			box.text += id[i];
			Debug.Log (box.text);
			box.text = "; ";
			box.text += answerKey[i];
			box.text = "; ";
			box.text += musicid[i];
			box.text = "; ";
			for (int j = 0; j < 10; j++) {
				box.text += answers [i, j];
				box.text += ", ";
			}
			box.text = "\n";
			Debug.Log (box.text);
			//Debug.Log(input.GetComponent<DatabaseManager2> ().id[i]);*/
		}
	}
}
