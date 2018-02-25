using UnityEngine;
using System;
using System.IO;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using Mono.Data.SqliteClient; 


public class DatabaseManager : MonoBehaviour {

	public int[] id = new int[26];
	public int[] musicid = new int[26];
	public string[] answerKey = new string[26];
	public string[,] answers = new string[26,10];

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
		string filepath = "jar:file://" + Application.dataPath + "!/assets/datas.db"; // System.IO.Path.Combine(Application.streamingAssetsPath, "datas.s3db");
		string newpath = Application.persistentDataPath + "/datas.db";
		//string newpath = Application.streamingAssetsPath + "/datas.db";
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
		else 
			Debug.Log("file doesnt exist" );
		conn = "URI=file:" + newpath; //Path to database.
		dbconn = new SqliteConnection(conn);
		dbconn.Open(); //Open connection to the database.

		//DB READ
		dbcmd = dbconn.CreateCommand();
		sqlQuery = "SELECT ID, MusicID, AnswerKey, A1, A2, A3, A4, A5, A6, A7, A8, A9, A10 " + "FROM Quiz";
		dbcmd.CommandText = sqlQuery;
		reader = dbcmd.ExecuteReader();
		int n = 1;
		while (reader.Read())
		{
			id [n] = reader.GetInt32 (0);
			musicid [n] = reader.GetInt32 (1);
			answerKey [n] = reader.GetString (2);
			for(int i = 0; i<10;i++)
				answers [n,i] = reader.GetString (3+i);
			n++;
		}

		//DB CLOSE
		reader.Close();
		reader = null;
		dbcmd.Dispose();
		dbcmd = null;
		dbconn.Close();
		dbconn = null;

		for(int i = 1; i< 26; i++){
			Debug.Log (id [i] + " " + answerKey [i] + " ");
			for (int j = 0; j < 10; j++)
				Debug.Log (answers [i, j] + " ");
		}

		GameObject.Find ("Text2").GetComponent<answer>().databaseWrite ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
