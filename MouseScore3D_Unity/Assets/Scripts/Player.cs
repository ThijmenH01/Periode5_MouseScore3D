using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public delegate void OnOffoadAction(bool onRoad);
    public static event OnOffoadAction OnOffroad;

    public Transform playerCar;
    public LayerMask layerMask;
    public AudioSource horn;
    public AudioSource engineSound;
    public float health = 100;

    private float healthAddon = 0.1f;
    private bool isMaxHealth = true;
    private GameManager gameManager;
    private int carLightNormal = 10;
    private int carLightShineBU;
    private int carLightShine = 75;

    [SerializeField] private float damage;
    [SerializeField] private Light frontR;
    [SerializeField] private Light frontL;

    private void Awake() {
        gameManager = GameManager.instance;
        horn = GetComponent<AudioSource>();
    }

    private void Start() {
        NotificationCenter.OnPauseEvent += GamePauseHandler;
        NotificationCenter.OnUnPauseEvent += GameUnPauseHandler;
        NotificationCenter.OnGameOverEvent += GameOverHandler;
        NotificationCenter.OnNextLevelEvent += NextLevelHandler;
        NotificationCenter.OnCheatEvent += CheatHandler;

        //StartCoroutine( HealingAsync( 0.1f ) );

        carLightShineBU = carLightShine;
    }

    void Update() {

        if(health >= 100) {
            isMaxHealth = true;
        } else {
            isMaxHealth = false;
        }

        if(Input.GetKeyDown( KeyCode.Equals )) {
            CheatHandler();
        }

        Movement();
        VehicleOnRoad();
        Horn();

        if(!VehicleOnRoad() && !gameManager.gameIsPreparing) {
            OffRoad();
        }
    }

    private void GamePauseHandler() {
        carLightShine = carLightNormal;
        horn.volume = 0;
        engineSound.Pause();
    }

    private void GameUnPauseHandler() {
        carLightShine = carLightShineBU;
        horn.volume = 1;
        engineSound.Play();
    }

    private void GameOverHandler() {
        carLightShine = carLightNormal;
        horn.volume = 0;
        engineSound.Stop();
    }

    private void NextLevelHandler() {
        float healthRegen = 20;

        if(!isMaxHealth) {
            health += healthRegen;
            health = Mathf.Clamp( health , 0 , 100 );
        }
    }

    private void CheatHandler () {
        health += 25;
        health = Mathf.Clamp( health , 0 , 100 );
    }

    private void Movement() {
        transform.position = transform.position + new Vector3( Input.GetAxisRaw( "Mouse X" ) * Time.deltaTime , 0 , 0 );
        Quaternion _rot = Quaternion.Euler( new Vector3( transform.eulerAngles.x , Input.GetAxisRaw( "Mouse X" ) * 20 , transform.eulerAngles.z ) );
        transform.rotation = Quaternion.Slerp( transform.rotation , _rot , Time.deltaTime * 10 );
    }

    public bool VehicleOnRoad() {
        Ray ray = new Ray( transform.position , -transform.up );
        RaycastHit hitInfo;

        if(Physics.Raycast( ray , out hitInfo , 100 )) {
            if(hitInfo.collider.tag == "Road") {
                Debug.DrawLine( ray.origin , hitInfo.point , Color.green );
                OnOffroad?.Invoke( false );
                return true;

            } else {
                Debug.DrawLine( ray.origin , ray.origin + ray.direction * 100 , Color.red );
                return false;
            }
        } else {
            return false;
        }
    }

    private void OffRoad() {
        health -= damage * Time.deltaTime;
        OnOffroad?.Invoke( true );
    }

    private void Horn() {
        if(Input.GetMouseButtonDown( 0 )) {
            horn.Play( 0 );
            frontR.intensity = carLightShine;
            frontL.intensity = carLightShine;
        }
        if(Input.GetMouseButtonUp( 0 )) {
            horn.Stop();
            frontR.intensity = carLightNormal;
            frontL.intensity = carLightNormal;
        }
    }

    //Not Using
    private IEnumerator HealingAsync(float time) {
        while(true) {
            if(!isMaxHealth) {
                health += healthAddon;
            }
            yield return new WaitForSeconds( time );
        }
    }

    private void OnDestroy() {
        NotificationCenter.OnPauseEvent -= GamePauseHandler;
        NotificationCenter.OnUnPauseEvent -= GameUnPauseHandler;
        NotificationCenter.OnGameOverEvent -= GameOverHandler;
        NotificationCenter.OnNextLevelEvent -= NextLevelHandler;
        NotificationCenter.OnCheatEvent -= CheatHandler;
    }
}
