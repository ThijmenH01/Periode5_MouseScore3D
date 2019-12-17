using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MetricsSaver : MonoBehaviour {

    private void Awake() {
        NotificationCenter.OnSaveEvent += Save;
        NotificationCenter.OnLoadEvent += Load;
        SaveObject saveObject = new SaveObject {
            highScore = 0 ,
            timesPlayed = 0,
            totalDistanceDriven = 0,
        };
        string json = JsonUtility.ToJson( saveObject );

        SaveObject loadSavedObject = JsonUtility.FromJson<SaveObject>( json );
        MetricSaveSystem.Init();
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
