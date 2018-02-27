﻿using UnityEngine;
using System;
using System.IO;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using Mono.Data.SqliteClient; 
using UnityEngine.UI;

public class DatabaseManager : MonoBehaviour {

	public int n;
	public List<int> score;
	private string conn;
	private string sqlQuery;
	private IDbConnection dbconn;
	private IDbCommand dbcmd;
	private IDataReader reader;
	private StringBuilder builder;

	void Start () {
		score = new List<int>();

		#if UNITY_STANDALONE_WINDOWS || UNITY_EDITOR

		string newpath = Application.dataPath + "/StreamingAssets/datas.db";

		#elif UNITY_ANDROID

		string filepath = "jar:file://" + Application.dataPath + "!/assets/datas.db"; 
		string newpath = Application.persistentDataPath + "/datas.db";
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
		sqlQuery = "SELECT Score " + "FROM HiScore";
		dbcmd.CommandText = sqlQuery;
		reader = dbcmd.ExecuteReader();

		n = 0;

		while (reader.Read())
		{
			score.Add(reader.GetInt32(0));
			n++;

		}
		sortScore ();
	
		//DB CLOSE
		reader.Close();
		reader = null;
		dbcmd.Dispose();
		dbcmd = null;
		dbconn.Close();
		dbconn = null;

		HiScorePost ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void sortScore(){
		int temp;
		for (int i = n-1; i > 0; i--) {
			for (int j = i - 1; j >= 0; j--) {
				if (score [i] > score [j]) {
					temp = score [i];
					score [i] = score [j];
					score [j] = temp;
				}
			}
		}
	}
	public void HiScorePost(){
		GameObject.Find ("ScoreBox").GetComponent<Text> ().text = score [0].ToString ();
	}
}
