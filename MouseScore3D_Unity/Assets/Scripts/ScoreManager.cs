using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public static ScoreManager instance;

    public delegate void OnNextLevelAction();
    public event OnNextLevelAction OnNextLevel;

    public Text scoreInTimeText;
    public int currentLevel = 1;

    [SerializeField] private LevelUpUI levelUpUI;
    private int scoreInTime;
    private bool allowedToSwitchLevel = false;

    private void Start() {
        instance = this;
        StartCoroutine( ScoreAsync( 0.1f ) );
    }

    private IEnumerator ScoreAsync(float time) {
        while(true) {
            yield return new WaitForSeconds( time );
            if(!GameManager.instance.gameIsPreparing) {
                scoreInTime++;
                scoreInTimeText.text = "M: " + scoreInTime.ToString("D5");

                if(scoreInTime % 100 == 0 && !GameManager.instance.gameIsPreparing) {
                    allowedToSwitchLevel = true;
                    if(allowedToSwitchLevel) {
                        NextLevelReached();
                    }
                }
            }
        }
    }

    private void NextLevelReached() {
        currentLevel++;
        allowedToSwitchLevel = false;
        levelUpUI.SetUIState( true );
        levelUpUI.levelReachedText.text = "Reached Level " + currentLevel.ToString() + "!";
        OnNextLevel?.Invoke();
    }
}
