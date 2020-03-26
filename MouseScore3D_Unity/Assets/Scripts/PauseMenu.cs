using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public void GoToMainMenu() {
        SceneManager.LoadScene( 1 );
    }

    public void ExitGame() {
        Application.Quit();
    }

    public void ResumeGame() {
        GameManager.instance.ResumeGame();
    }
}
