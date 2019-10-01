using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public Text scoreInTimeText;

    private int scoreInTime;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        StartCoroutine(ScoreAsync(1));
    }

    private IEnumerator ScoreAsync(float time) {
        while(true) {
            yield return new WaitForSeconds(time);
            scoreInTime++;
            scoreInTimeText.text = scoreInTime.ToString();
        }
    }
}
