using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginInput : MonoBehaviour {

    public Api loginManager;

    //public string username;
    //public string password;

    public InputField usernameInputField;
    public InputField passwordInputField;

    public void Login() {
        loginManager.debugEmail = usernameInputField.text;
        loginManager.debugPassword = passwordInputField.text;
        StartCoroutine( loginManager.LoginRequestAsync() );
    }

    public void Create() {
        loginManager.debugPassword = passwordInputField.text;
        loginManager.debugPassword = passwordInputField.text;
        StartCoroutine( loginManager.CreateAccountRequestAsync() );
    }
}
