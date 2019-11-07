using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    //Events
    public delegate void OnGameStart();
    public event OnGameStart OnGameStartEvent;
    public delegate void OnPause();
    public event OnPause OnPauseEvent;
    public delegate void OnUnpause();
    public event OnPause OnUnPauseEvent;
    public delegate void OnGameOver();
    public event OnGameOver OnGameOverEvent;

    //Public Variables
    public Text timeTillStartText;
    public Text reachedLevelGameOverText;
    public GameObject gameOverScreen;
    public Player player;
    public bool gameIsOver = false;
    public bool gameIsPreparing = true;
    public bool gameIsPaused = false;

    //Private Variables
    [SerializeField] private int timeTillStart;
    [SerializeField] private GameObject pauseMenu;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        StartCoroutine( StartCountdown( 1 ) );

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update() {

        if(!gameIsPreparing) {
            OnGameStartEvent?.Invoke();
        }

        if(player.health <= 0) {
            GameOver();
        }

        if(Input.GetKeyDown( KeyCode.Escape ) && !gameIsOver) {
            if(!gameIsPaused)
                PauseGame();
            else
                ResumeGame();
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

    public void GameOver() {
        gameIsOver = true;
        gameOverScreen.SetActive( true );
        reachedLevelGameOverText.text = "Game Over, You reached level " + ScoreManager.instance.currentLevel + "!";
        player.health = Mathf.Clamp( player.health , 0 , 100 );

        if(Input.GetKeyDown(KeyCode.Space) && gameIsOver) {
            RestartGame();
        }
        OnGameOverEvent?.Invoke();
    }

    private void PauseGame() {
        gameIsPaused = true;
        pauseMenu.SetActive( true );
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        OnPauseEvent?.Invoke();
    }

    public void ResumeGame() {
        Cursor.lockState = CursorLockMode.Locked;
        gameIsPaused = false;
        pauseMenu.SetActive( false );
        Time.timeScale = 1;
        OnUnPauseEvent?.Invoke();
    }

    public void RestartGame() {
        Time.timeScale = 1;
        if(gameIsOver) {
            gameIsOver = false;
        }
        SceneManager.LoadScene( 1 );
    }
}
