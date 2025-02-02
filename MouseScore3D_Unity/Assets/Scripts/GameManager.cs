﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    public Api api;

    public Text highScoreText;
    public Text timeTillStartText;
    public Text reachedLevelGameOverText;
    public GameObject gameOverScreen;
    public Player player;
    public bool gameIsOver = false;
    public bool gameIsPreparing = true;
    public bool gameIsPaused = false;

    [SerializeField] private int timeTillStart;
    [SerializeField] private GameObject pauseMenu;
    private bool isSaved = false;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        NotificationCenter.OnGameOverEvent += GameOverHandler;
        api = FindObjectOfType<Api>();
        StartCoroutine( StartCountdown( 1 ) );
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update() {

        if(!gameIsPreparing) {
            NotificationCenter.FireGameStart();
        }

        if(Input.GetKeyDown( KeyCode.R )) {
            RestartGame();
        }

        if(Input.GetKeyDown( KeyCode.Escape ) && !gameIsOver) {
            if(!gameIsPaused)
                PauseGame();
            else
                ResumeGame();
        }

        if(Input.GetKeyDown( KeyCode.S )) {
            api.Save();
        }
    }

    private IEnumerator StartCountdown(float time) {
        while(true) {
            yield return new WaitForSeconds( time );
            if(gameIsPreparing) {
                timeTillStart--;
                timeTillStartText.text = timeTillStart.ToString();
                if(timeTillStart <= 0) {
                    gameIsPreparing = false;
                }
            }
        }
    }

    public void GameOverHandler() {
        gameIsOver = true;
        gameOverScreen.SetActive( true );
        reachedLevelGameOverText.text = "Game Over, You reached level " + ScoreManager.instance.currentLevel + "!";
        player.health = Mathf.Clamp( player.health , 0 , 100 );
        if(!isSaved) {
            isSaved = true;
            highScoreText.text = "Highscore: " + api.highscore + "!";
            //GlobalStats.totalDistanceDriven += ScoreManager.instance.scoreInTime;
            NotificationCenter.FireSave();
        }
        //NotificationCenter.FireGameOver();
        if(Input.GetKeyDown( KeyCode.Space ) && gameIsOver) {
            RestartGame();
        }
    }

    private void PauseGame() {
        gameIsPaused = true;
        pauseMenu.SetActive( true );
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        NotificationCenter.FireGamePause();
        Cursor.visible = true;
    }

    public void ResumeGame() {
        Cursor.lockState = CursorLockMode.Locked;
        gameIsPaused = false;
        pauseMenu.SetActive( false );
        Time.timeScale = 1;
        NotificationCenter.FireGameUnPause();
        Cursor.visible = false;
    }

    public void RestartGame() {
        Time.timeScale = 1;
        if(gameIsOver) {
            gameIsOver = false;
        }
        SceneManager.LoadScene( 2 );
    }

    private void OnDestroy() {
        NotificationCenter.OnGameOverEvent -= GameOverHandler;
    }
}
