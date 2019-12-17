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
        };
        string json = JsonUtility.ToJson( saveObject );

        SaveObject loadSavedObject = JsonUtility.FromJson<SaveObject>( json );
        MetricSaveSystem.Init();
    }

    public static void Save() {
        SaveObject saveObject = new SaveObject {
            highScore = ScoreManager.instance.highScore ,
        };
        string json = JsonUtility.ToJson( saveObject );
        MetricSaveSystem.Save( json );
        Debug.Log( "Saved score: " + saveObject.highScore );
    }

    public static void Load() {
        string saveString = MetricSaveSystem.Load();
        if(saveString != null) {
            SaveObject saveObject = JsonUtility.FromJson<SaveObject>( saveString );
            ScoreManager.instance.highScore = saveObject.highScore;
            Debug.Log( "Loaded score: " + saveObject.highScore );
        } else {
            Debug.LogError( "Didnt Load" );
        }
    }

    private class SaveObject {
        public int highScore;
    }

    private void OnDestroy() {
        NotificationCenter.OnSaveEvent -= Save;
        NotificationCenter.OnLoadEvent -= Load;

    }
}
