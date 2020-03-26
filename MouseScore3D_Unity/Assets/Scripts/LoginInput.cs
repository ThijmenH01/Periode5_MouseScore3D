using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginInput : MonoBehaviour {

    public static LoginInput instance;

    public Api loginManager;
    public GameObject loggininScreen;
    public Text loggininScreenText;
    [SerializeField] public string popupContent;

    public InputField usernameInputField;
    public InputField passwordInputField;

    private void Start() {
        instance = this;
        NotificationCenter.OnLogginInEvent += LogginInAction;
    }

    public void Login() {
        loginManager.debugEmail = usernameInputField.text;
        loginManager.debugPassword = passwordInputField.text;
        StartCoroutine( loginManager.LoginRequestAsync() );
    }

    public void Create() {
        loginManager.debugEmail = usernameInputField.text;
        loginManager.debugPassword = passwordInputField.text;
        StartCoroutine( loginManager.CreateAccountRequestAsync() );
    }

    public void LogginInAction() {
        loggininScreenText.text = popupContent.ToString();
        loggininScreen.SetActive( true );
    }

    public void OnDestroy() {
        loggininScreen.SetActive( false );
        NotificationCenter.OnLogginInEvent -= LogginInAction;
    }
}
