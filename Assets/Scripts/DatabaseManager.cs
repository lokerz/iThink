using UnityEngine;
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

		#if UNITY_STANDALONE_WIN || UNITY_EDITOR

			string newpath = Application.dataPath + "/StreamingAssets/datas.db";

		#elif UNITY_ANDROID
			string filepath = "jar:file://" + Application.dataPath + "!/assets/datas.db"; 
			string newpath = Application.persistentDataPath + "/datas.db";
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
		sqlQuery = "SELECT Score " + "FROM HiScore";
		dbcmd.CommandText = sqlQuery;
		reader = dbcmd.ExecuteReader();

		n = 0;

		while (reader.Read())
		{
			
			score.Add(reader.GetInt32(0));
			n++;

		}
	
		//DB CLOSE
		reader.Close();
		reader = null;
		dbcmd.Dispose();
		dbcmd = null;
		dbconn.Close();
		dbconn = null;
	}

	public void HiScorePost(){
		for (int i = 0; i < n; i++) {
			GameObject.Find ("Text"+(i+1).ToString()).GetComponent<Text> ().text = "Stage " + (i+1) + "\n" + score [i].ToString () + "/100";
		}
	}
}
