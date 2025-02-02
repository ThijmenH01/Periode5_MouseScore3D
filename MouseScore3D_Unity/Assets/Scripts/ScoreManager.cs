﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public static ScoreManager instance;

    public Text scoreInTimeText;
    public int scoreInTime;
    public int currentLevel = 1;

    [SerializeField] private LevelUpUI levelUpUI;
    private bool allowedToSwitchLevel = false;
    private bool isSaved = false;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        NotificationCenter.OnGameOverEvent += GameOverHandler;
        NotificationCenter.OnNextLevelEvent += NextLevelReachedHandler;
        NotificationCenter.OnCheatEvent += CheatHandler;

        NotificationCenter.FireLoad();

        StartCoroutine( ScoreAsync( 0.1f ) );
        StartCoroutine( TimePlayedAsync() );
    }

    private void Update() {
        if(Input.GetKeyDown( KeyCode.Minus )) {
            CheatHandler();
        }

        if(GlobalStats.highScore <= scoreInTime) {
            GlobalStats.highScore = scoreInTime;
        }
    }

    private IEnumerator ScoreAsync(float time) {
        while(true) {
            yield return new WaitForSeconds( time );
            if(!GameManager.instance.gameIsPreparing) {
                scoreInTime++;
                scoreInTimeText.text = "M: " + scoreInTime.ToString( "D4" );

                if(scoreInTime % 100 == 0 && !GameManager.instance.gameIsPreparing) {
                    allowedToSwitchLevel = true;
                    if(allowedToSwitchLevel) {
                        NotificationCenter.FireNextLevelReached();
                    }
                }
            }
        }
    }

    private void NextLevelReachedHandler() {
        currentLevel++;
        allowedToSwitchLevel = false;
        levelUpUI.SetUIState( true );
        levelUpUI.levelReachedText.text = "Reached Level " + currentLevel.ToString() + "!";
    }

    private void CheatHandler() {
        NotificationCenter.FireNextLevelReached();
    }

    private void GameOverHandler() {
        //FIX
        GlobalStats.totalDistanceDriven += scoreInTime;
        //print( totalDistanceDriven );

        scoreInTimeText.enabled = false;
        //NotificationCenter.FireSave();
        if(!isSaved) {
            //MetricsSaver.Save();
            isSaved = true;
        }
    }

    private IEnumerator TimePlayedAsync() {
        while(true) {
            GlobalStats.timePlayed++;
            yield return new WaitForSeconds( 1 );
        }
    }

    void OnDestroy() {
        NotificationCenter.OnGameOverEvent -= GameOverHandler;
        NotificationCenter.OnNextLevelEvent -= NextLevelReachedHandler;
        NotificationCenter.OnCheatEvent -= CheatHandler;
    }
}
