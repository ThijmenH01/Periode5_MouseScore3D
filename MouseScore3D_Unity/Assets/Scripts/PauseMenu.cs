using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public void GoToMainMenu() {
        SceneManager.LoadScene( 0 );
    }

    public void ExitGame() {
        Application.Quit();
    }

    public void ResumeGame() {
        GameManager.instance.ResumeGame();
    }

    public void ClearStats() {
        GlobalStats.highScore = 0;
        NotificationCenter.FireSave();
    }
}
