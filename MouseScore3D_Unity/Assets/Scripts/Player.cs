using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public Event makeHPBarShakeAction;
    public Transform playerCar;
    public LayerMask layerMask;
    public AudioSource horn;
    public float health = 100;

    private GameManager gameManager;
    private Vector3 startPos;
    [SerializeField] private float damage;
    [SerializeField] private HealthBar healthBar;

    public delegate void OnOffoadAction(bool onRoad);
    public static event OnOffoadAction OnOffroad;

    private void Awake() {
        startPos = transform.position;
        gameManager = GameManager.instance;
        horn = GetComponent<AudioSource>();
    }

    private void Start() {
        transform.position = startPos;
    }

    void Update() {
        Movement();
        VehicleOnRoad();
        Horn();

        if (!VehicleOnRoad() && !gameManager.gameIsPreparing) {
            OffRoad();
        }
    }

    private void Movement() {
        transform.position = transform.position + new Vector3( Input.GetAxisRaw( "Mouse X" ) * Time.deltaTime , 0 , 0 );
        Quaternion _rot = Quaternion.Euler( new Vector3( transform.eulerAngles.x , Input.GetAxisRaw( "Mouse X" ) * 20 , transform.eulerAngles.z ) );
        transform.rotation = Quaternion.Slerp( transform.rotation , _rot , Time.deltaTime * 10 );
    }

    public bool VehicleOnRoad() {
        Ray ray = new Ray( transform.position , -transform.up );
        RaycastHit hitInfo;

        if (Physics.Raycast( ray , out hitInfo , 100 )) {
            if (hitInfo.collider.tag == "Road") {
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
        OnOffroad?.Invoke(true);
    }

    private void Horn() {
        if (Input.GetMouseButtonDown( 0 )) {
            horn.Play( 0 );
        }
        if (Input.GetMouseButtonUp( 0 )) {
            horn.Stop();
        }
    }
}
