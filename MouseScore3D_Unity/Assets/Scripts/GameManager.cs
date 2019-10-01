using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    public Text scoreInTimeText;
    public Player player;
    public bool gameOver = false;

    private int scoreInTime;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        StartCoroutine(ScoreAsync(1));
    }

    private void Update() {
        if(player.health <= 0) {
            EndGame();
        }
    }

    private IEnumerator ScoreAsync(float time) {
        while(true) {
            yield return new WaitForSeconds(time);
            scoreInTime++;
            scoreInTimeText.text = scoreInTime.ToString();
        }
    }

    private void CalculateScore() {
                 
    }

    private void EndGame() {
        gameOver = true;
        player.health = Mathf.Clamp(player.health , 0 , 100);
    }
}
