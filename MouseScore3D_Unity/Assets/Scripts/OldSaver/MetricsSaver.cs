﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
//using MySql.Data.MySqlClient;


//public class MetricsSaver : MonoBehaviour {

//    public string url;

//    private void Awake() {
//        NotificationCenter.OnSaveEvent += Save;
//        NotificationCenter.OnLoadEvent += Load;

//        SaveObject saveObject = new SaveObject {
//            //====
//            highScore = 0 ,
//            timesPlayed = 0 ,
//            totalDistanceDriven = 0 ,
//            timePlayed = 0 ,
//            //====
//        };

//    }

//    private void Start() {


//    }

//    public void Save() {
//        StartCoroutine( SaveAsync() );
//    }

//    public void Load() {
//        StartCoroutine( LoadAsync() );
//    }

//    public IEnumerator SaveAsync() {
//        ChangeHighscoreRequest request = new ChangeHighscoreRequest();
//        string json = JsonUtility.ToJson( request );
//        WWWForm form = new WWWForm();
//        form.AddField( "request" , json );
//        using(UnityWebRequest www = UnityWebRequest.Post( url , form )) {
//            yield return www.SendWebRequest();
//            if(www.isNetworkError || www.isHttpError) {
//                Debug.Log( www.error );
//            } else {
//                Debug.Log( www.downloadHandler.text );
//            }
//        }
//    }

//    private IEnumerator LoadAsync() {
//        ChangeHighscoreRequest request = new ChangeHighscoreRequest();
//        string json = JsonUtility.ToJson( request );
//        WWWForm form = new WWWForm();
//        form.AddField( "request" , json );
//        using(UnityWebRequest www = UnityWebRequest.Post( url , form )) {
//            yield return www.SendWebRequest();
//            if(www.isNetworkError || www.isHttpError) {
//                Debug.Log( www.error );
//            } else {
//                Debug.Log( www.downloadHandler.text );
//            }
//        }
//    }

//    private class SaveObject {
//        //====
//        public int highScore;
//        public int timesPlayed;
//        public int totalDistanceDriven;
//        public int timePlayed;
//        //====
//    }

//    private void OnDestroy() {
//        NotificationCenter.OnSaveEvent -= Save;
//        NotificationCenter.OnLoadEvent -= Load;
//    }
//}

//[System.Serializable]
//public class ChangeHighscoreRequest : BaseRequest {
//    public ChangeHighscoreRequest() {
//        action = "update_highscore";
//    }
//    public string token;
//    public int highscore = 100;
//}





















//string json = JsonUtility.ToJson( saveObject );

//SaveObject loadSavedObject = JsonUtility.FromJson<SaveObject>( json );
//MetricSaveSystem.Init();

//private static MySqlCommand _resultInsert;
//private static MySqlCommand _resultUpdate;
//private static MySqlCommand _resultCheck;
//string userName = "test1234567";

//string cs = @"server=remotemysql.com;Port=3306;userid=vDo2aRlMc9;password=muHNFQDxle;database=vDo2aRlMc9";
//MySqlConnection con = new MySqlConnection( cs );
//con.Open();

////string _command1 = "INSERT INTO db (user, besttime) VALUES ('test1', 39);";
//string _commandInsert = "INSERT INTO db (user, besttime, totaltime, timesplayed, totaldistance) VALUES ('" + userName + "', " + GlobalStats.highScore + ", " + GlobalStats.timePlayed + ", " + GlobalStats.timesPlayed + ", " + GlobalStats.timePlayed + ");";
//string _commandUpdate = "UPDATE db SET besttime = " + GlobalStats.highScore + " , totaltime = " + GlobalStats.timePlayed + ", timesplayed = " + GlobalStats.timesPlayed + ", totaldistance = " + GlobalStats.totalDistanceDriven + ";";
//string _commandCheck = "SELECT 'user' FROM db WHERE user = '" + userName + "'";

//_resultInsert = new MySqlCommand( _commandInsert , con );
//_resultUpdate = new MySqlCommand( _commandUpdate , con );
//_resultCheck = new MySqlCommand( _commandCheck , con );

////FIX
////_resultCheck = new MySqlCommand( _commandCheck , con );
////Debug.Log( _resultCheck );
////Debug.Log( $"MySQL Ping : {con.Ping()}" );

////_resultInsert.ExecuteNonQuery();
////_resultUpdate.ExecuteNonQuery();
//bool _abort = true;
//using(MySqlDataReader _checkReader = _resultCheck.ExecuteReader()) {
//    while(_checkReader.Read()) {
//        if(_checkReader.GetString( 0 ) != null) {
//            //Debug.Log( _checkReader.GetString( 0 ) );
//            Debug.Log( "Gebruiker gevonden, geupdate" );
//            _abort = false;
//            _resultUpdate.ExecuteNonQuery();
//        }
//    }
//}

//if(_abort) {
//    Debug.Log( "Gebruiker niet gevonden, nieuwe aangemaakt" );
//    _resultInsert.ExecuteNonQuery();
//}

//SaveObject saveObject = new SaveObject {
//    //====
//    highScore = GlobalStats.highScore ,
//    timesPlayed = GlobalStats.timesPlayed ,
//    totalDistanceDriven = GlobalStats.totalDistanceDriven ,
//    timePlayed = GlobalStats.timePlayed ,
//    //====
//};
//string json = JsonUtility.ToJson( saveObject );
//MetricSaveSystem.Save( json );

//FIX
//_resultInsert.ExecuteNonQuery();

//}

//string saveString = MetricSaveSystem.Load();
//if(saveString != null) {
//    SaveObject saveObject = JsonUtility.FromJson<SaveObject>( saveString );
//    //====
//    GlobalStats.highScore = saveObject.highScore;
//    GlobalStats.timesPlayed = saveObject.timesPlayed;
//    GlobalStats.totalDistanceDriven = saveObject.totalDistanceDriven;
//    GlobalStats.timePlayed = saveObject.timePlayed;
//    //====
//} else {
//    Debug.LogError( "Didnt Load, savestring = " + saveString );


