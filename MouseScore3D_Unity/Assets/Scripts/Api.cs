﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
public class Api : MonoBehaviour {

    public string url = "http://127.0.0.1/edsa-Database/LoginManager.php";
    public string token;
    public int highscore;
    [Header( "Data" )]
    public string email;
    public string password;


    private void Awake() {
        NotificationCenter.OnSaveEvent += Save;
        //NotificationCenter.OnLoadEvent += Load;
    }

    private void Start() {
        DontDestroyOnLoad( this.gameObject );
    }

    private void Update() {

        //if(Input.GetKeyDown( KeyCode.C )) {
        //    StartCoroutine( CreateAccountRequestAsync() );
        //}

        //if(Input.GetKeyDown( KeyCode.L )) {
        //    StartCoroutine( LoginRequestAsync() );
        //}

        //if(Input.GetKeyDown( KeyCode.O )) {
        //    StartCoroutine( LogoutRequestAsync() );
        //}
    }

    public IEnumerator CreateAccountRequestAsync() {
        LoginInput.instance.popupContent = "Creating Account...";
        NotificationCenter.FireLogginIn();
        CreateAccountRequest request = new CreateAccountRequest();
        request.email = email;
        request.password = password;
        string json = JsonUtility.ToJson( request );
        WWWForm form = new WWWForm();
        form.AddField( "request" , json );
        using(UnityWebRequest www = UnityWebRequest.Post( url , form )) {
            yield return www.SendWebRequest();
            if(www.isNetworkError || www.isHttpError) {
                Debug.Log( www.error );
            } else {
                Debug.Log( www.downloadHandler.text );
                BaseResponse response = JsonUtility.FromJson<BaseResponse>( www.downloadHandler.text );
                switch(response.status) {
                    case "ok":
                        Debug.Log( "Account created" );
                        LoginInput.instance.popupContent = "Account Created";
                        NotificationCenter.FireLogginIn();
                        break;
                    case "email_already_exists":
                        Debug.Log( "Account already exists" );
                        LoginInput.instance.popupContent = "Already Exists";
                        NotificationCenter.FireLogginIn();
                        break;
                    case "error":
                        Debug.Log( response.errorMessage );
                        break;
                }
            }
        }
    }
    public IEnumerator LoginRequestAsync() {
        LoginInput.instance.popupContent = "Loggin In...";
        NotificationCenter.FireLogginIn();
        LoginRequest request = new LoginRequest();
        request.email = email;
        request.password = password;
        string json = JsonUtility.ToJson( request );
        WWWForm form = new WWWForm();
        form.AddField( "request" , json );
        using(UnityWebRequest www = UnityWebRequest.Post( url , form )) {
            yield return www.SendWebRequest();
            if(www.isNetworkError || www.isHttpError) {
                Debug.Log( www.error );
            } else {
                Debug.Log( www.downloadHandler.text );
                LoginResponse response = JsonUtility.FromJson<LoginResponse>( www.downloadHandler.text );
                switch(response.status) {
                    case "ok":
                        Debug.Log( "Token retrieved" );
                        token = response.token;
                        highscore = response.highscore;
                        LoggedInSuccesFully( 1 );
                        break;
                    case "wrong_email_or_password":
                        LoginInput.instance.popupContent = "Wrong Credentials";
                        NotificationCenter.FireLogginIn();
                        Debug.Log( "Wrong email or password" );
                        break;
                    case "error":
                        Debug.Log( response.errorMessage );
                        break;
                }
            }
        }
    }
    public IEnumerator LogoutRequestAsync() {
        LogoutRequest request = new LogoutRequest();
        request.email = email;
        request.token = token;
        string json = JsonUtility.ToJson( request );
        WWWForm form = new WWWForm();
        form.AddField( "request" , json );
        using(UnityWebRequest www = UnityWebRequest.Post( url , form )) {
            yield return www.SendWebRequest();
            if(www.isNetworkError || www.isHttpError) {
                Debug.Log( www.error );
            } else {
                Debug.Log( www.downloadHandler.text );
                BaseResponse response = JsonUtility.FromJson<BaseResponse>( www.downloadHandler.text );
                switch(response.status) {
                    case "ok":
                        Debug.Log( "Logout succesful" );
                        token = string.Empty;
                        break;
                    case "incorrect_token":
                        Debug.Log( "Incorrect token" );
                        break;
                    case "email_not_found":
                        Debug.Log( "Email not found" );
                        break;
                    case "error":
                        Debug.Log( response.errorMessage );
                        break;
                }
            }
        }
    }

    public void Save() {
        StartCoroutine( SaveAsync() );
    }

    //public void Load() {
    //    StartCoroutine( LoadAsync() );
    //}

    public IEnumerator SaveAsync() {
        print( ScoreManager.instance.scoreInTime + "before" );
        Debug.Log( token );
        ChangeHighscoreRequest request = new ChangeHighscoreRequest();
        request.token = token;
        if(ScoreManager.instance.scoreInTime > highscore) {
            print( ScoreManager.instance.scoreInTime + "after 1" );
            request.highscore = ScoreManager.instance.scoreInTime;
            highscore = request.highscore;
        }
        string json = JsonUtility.ToJson( request );
        Debug.Log( json );
        WWWForm form = new WWWForm();
        form.AddField( "request" , json );
        using(UnityWebRequest www = UnityWebRequest.Post( url , form )) {
            yield return www.SendWebRequest();
            if(www.isNetworkError || www.isHttpError) {
                Debug.Log( www.error );
            } else {
                Debug.Log( www.downloadHandler.text );
            }
        }
    }

    //private IEnumerator LoadAsync() {
    //    Debug.Log( token );
    //    GetHighscoreRequest request = new GetHighscoreRequest();
    //    request.token = token;
    //    string json = JsonUtility.ToJson( request );
    //    Debug.Log( json );
    //    WWWForm form = new WWWForm();
    //    form.AddField( "request" , json );
    //    using(UnityWebRequest www = UnityWebRequest.Post( url , form )) {
    //        yield return www.SendWebRequest();
    //        if(www.isNetworkError || www.isHttpError) {
    //            Debug.Log( www.error );
    //        } else {
    //            Debug.Log( www.downloadHandler.text );
    //            print( request.highscore );
    //        }
    //    }
    //}

    public void LoggedInSuccesFully(int scene) {
        SceneManager.LoadScene( scene );
    }
}

[System.Serializable]
public class BaseRequest {
    public string action;
    public string email;
}

[System.Serializable]
public class CreateAccountRequest : BaseRequest {
    public CreateAccountRequest() {
        action = "create_account";
    }
    public string password;
}

[System.Serializable]
public class LoginRequest : BaseRequest {
    public LoginRequest() {
        action = "login";
    }
    public string password;
}

[System.Serializable]
public class LogoutRequest : BaseRequest {
    public LogoutRequest() {
        action = "logout";
    }
    public string token;
}

[System.Serializable]
public class BaseResponse {
    public string status;
    public string errorMessage;
}

[System.Serializable]
public class LoginResponse : BaseResponse {
    public string token;
    public int highscore;
}

[System.Serializable]
public class ChangeHighscoreRequest : BaseRequest {
    public ChangeHighscoreRequest() {
        action = "update_highscore";
    }
    public string token;
    public int highscore;
}

//public class GetHighscoreRequest : BaseRequest {
//    public GetHighscoreRequest() {
//        action = "get_highscore";
//    }
//    public string token;
//    public int highscore;
//}
