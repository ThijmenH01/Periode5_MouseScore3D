using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using MySql.Data.MySqlClient;


public class MetricsSaver : MonoBehaviour {
    private static MySqlCommand _resultInsert;
    private static MySqlCommand _resultUpdate;
    private static MySqlCommand _resultCheck;
    string test = "johan";

    private void Awake() {
        NotificationCenter.OnSaveEvent += Save;
        NotificationCenter.OnLoadEvent += Load;

        SaveObject saveObject = new SaveObject {
            highScore = 0 ,
            timesPlayed = 0 ,
            totalDistanceDriven = 0 ,
        };
        string json = JsonUtility.ToJson( saveObject );

        SaveObject loadSavedObject = JsonUtility.FromJson<SaveObject>( json );
        MetricSaveSystem.Init();
    }

    private void Start() {
        
        string cs = @"server=remotemysql.com;Port=3306;userid=vDo2aRlMc9;password=muHNFQDxle;database=vDo2aRlMc9";
        MySqlConnection con = new MySqlConnection( cs );
        con.Open();

        //string _command1 = "INSERT INTO db (user, besttime) VALUES ('test1', 39);";
        string _commandInsert = "INSERT INTO db (user, besttime, totaltime, timesplayed, totaldistance) VALUES ('"+ test + "', " + GlobalStats.highScore + ", " + GlobalStats.timePlayed + ", " + GlobalStats.timesPlayed + ", " + GlobalStats.timePlayed + ");";
        string _commandUpdate = "UPDATE db SET besttime = "+ GlobalStats.highScore +" , totaltime = "+ GlobalStats.timePlayed +", timesplayed = " + GlobalStats.timesPlayed +", totaldistance = "+ GlobalStats.totalDistanceDriven +";";
        string _commandCheck = "SELECT EXISTS(SELECT 'user' FROM db WHERE user != '"+ test +"')";

        _resultInsert = new MySqlCommand( _commandInsert , con );
        _resultUpdate = new MySqlCommand( _commandUpdate , con );
        
        
        //FIX
        //_resultCheck = new MySqlCommand( _commandCheck , con );

        Debug.Log( $"MySQL version : {con.ServerVersion}" );
    }

    public static void Save() {
        SaveObject saveObject = new SaveObject {
            highScore = GlobalStats.highScore ,
            timesPlayed = GlobalStats.timesPlayed ,
            totalDistanceDriven = GlobalStats.totalDistanceDriven ,
            timePlayed = GlobalStats.timePlayed ,
        };
        string json = JsonUtility.ToJson( saveObject );
        MetricSaveSystem.Save( json );

        //FIX

        _resultInsert.ExecuteNonQuery();
        //_resultUpdate.ExecuteNonQuery();
    }

    public static void Load() {
        string saveString = MetricSaveSystem.Load();
        if(saveString != null) {
            SaveObject saveObject = JsonUtility.FromJson<SaveObject>( saveString );
            GlobalStats.highScore = saveObject.highScore;
            GlobalStats.timesPlayed = saveObject.timesPlayed;
            GlobalStats.totalDistanceDriven = saveObject.totalDistanceDriven;
            GlobalStats.timePlayed = saveObject.timePlayed;
        } else {
            Debug.LogError( "Didnt Load" );
        }
    }

    private class SaveObject {
        public int highScore;
        public int timesPlayed;
        public int totalDistanceDriven;
        public int timePlayed;
    }

    private void OnDestroy() {
        NotificationCenter.OnSaveEvent -= Save;
        NotificationCenter.OnLoadEvent -= Load;
    }
}
