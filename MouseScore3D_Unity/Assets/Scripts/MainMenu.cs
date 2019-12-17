using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start() {
        NotificationCenter.FireLoad();
    }

    public void PlayGame() {
        SceneManager.LoadScene( 1 );
        if(Time.timeScale != 1) {
            Time.timeScale = 1;
        }
    }

    public void ExitGame() {
        Application.Quit();
    }
}
