using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationCenter : MonoBehaviour
{
    //GameStart
    public delegate void OnGameStartAction();
    public static event OnGameStartAction OnGameStartEvent;
    public static void FireGameStart() {
        OnGameStartEvent?.Invoke();
    }

    //GamePause
    public delegate void OnGamePauseAction();
    public static event OnGamePauseAction OnPauseEvent;
    public static void FireGamePause() {
        OnPauseEvent?.Invoke();
    }
   
    //GameResume
    public delegate void OnGameUnPauseAction();
    public static event OnGameUnPauseAction OnUnPauseEvent;
    public static void FireGameUnPause() {
        OnUnPauseEvent?.Invoke();
    }
    
    //GameOver
    public delegate void OnGameOverAction();
    public static event OnGameOverAction OnGameOverEvent;
    public static void FireGameOver() {
        OnGameOverEvent?.Invoke();
    }

    //NextLevel
    public delegate void OnNextLevelAction();
    public static event OnNextLevelAction OnNextLevelEvent;
    public static void FireNextLevelReached() {
        OnNextLevelEvent?.Invoke();
    }

    //Cheat
    public delegate void OnCheatAction();
    public static event OnCheatAction OnCheatEvent;
    public static void FireCheat() {
        OnCheatEvent?.Invoke();
    }

    //SaveStats
    public delegate void OnSaveAction();
    public static event OnSaveAction OnSaveEvent;
    public static void FireSave() {
        OnSaveEvent?.Invoke();
    }
   
    //LoadStats
    public delegate void OnLoadAction();
    public static event OnLoadAction OnLoadEvent;
    public static void FireLoad() {
        OnLoadEvent?.Invoke();
    }

    //Login
    public delegate void OnLogginInAction();
    public static event OnLoadAction OnLogginInEvent;
    public static void FireLogginIn() {
        OnLogginInEvent?.Invoke();
    }
}
