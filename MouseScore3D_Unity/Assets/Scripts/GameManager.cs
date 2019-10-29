using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    public Text scoreInTimeText;
    public Text timeTillStartText;
    public Player player;
    public bool gameOver = false;
    public bool gameIsPreparing = true;

    [SerializeField] private int timeTillStart;
    private int scoreInTime;
    private int score;
    private int scoreMultiplier = 100;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        StartCoroutine( StartCountdown( 1 ) );
        StartCoroutine( ScoreAsync( 0.1f ) );
    }

    private void Update() {

        if(!gameIsPreparing) {
            MoveTextAway(100);
        }

        if(player.health <= 0) {
            EndGame();
        }
    }

    private IEnumerator ScoreAsync(float time) {
        while(true) {
            yield return new WaitForSeconds( time );
            if(!gameIsPreparing) {
                scoreInTime++;
                scoreInTimeText.text = "M: " + scoreInTime.ToString();
            }
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

    private void CalculateScore() {
        score = scoreInTime * scoreMultiplier;
    }

    private void EndGame() {
        gameOver = true;
        player.health = Mathf.Clamp( player.health , 0 , 100 );
        CalculateScore();
    }

    private void MoveTextAway(float moveSpeed) {
        timeTillStartText.transform.Translate( Vector2.up * moveSpeed * Time.deltaTime );
    }
}
