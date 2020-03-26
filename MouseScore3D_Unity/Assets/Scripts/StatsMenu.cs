using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsMenu : MonoBehaviour
{
    public Text highscoreText;
    public Api api;

    private void Start() {
        api = FindObjectOfType<Api>();
        highscoreText.text = "Highscore in Meters: " + api.highscore.ToString();
    }
}
