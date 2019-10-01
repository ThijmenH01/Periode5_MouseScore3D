using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public Transform playerCar;
    public LayerMask layerMask;
    public AudioSource horn;
    public float health = 100;

    private GameManager gameManager;
    [SerializeField] private float damage;
    [SerializeField] private HealthBar healthBar;

    private void Awake() {
        gameManager = GameManager.instance;
        horn = GetComponent<AudioSource>();
    }

    void Update() {
        Movement();
        VehicleOnRoad();
        Horn();

        if(!VehicleOnRoad()) {
            health -= damage * Time.deltaTime;
            healthBar.SetSize(health / 100);
        }
    }

    private void Movement() {
        transform.position = transform.position + new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime , 0 , 0);
        Quaternion _rot = Quaternion.Euler(new Vector3(transform.eulerAngles.x , Input.GetAxisRaw("Mouse X") * 20 , transform.eulerAngles.z));
        transform.rotation = Quaternion.Slerp(transform.rotation , _rot , Time.deltaTime * 10);
    }

    public bool VehicleOnRoad() {
        Ray ray = new Ray(transform.position , -transform.up);
        RaycastHit hitInfo;

        if(Physics.Raycast(ray , out hitInfo , 100)) {
            if(hitInfo.collider.tag == "Road") {
                Debug.DrawLine(ray.origin , hitInfo.point , Color.green);
                return true;

            } else {
                Debug.DrawLine(ray.origin , ray.origin + ray.direction * 100 , Color.red);
                return false;
            }
        } else {
            return false;
        }
    }

    private void Horn() {
        if(Input.GetMouseButtonDown(0)) {
            horn.Play(0);
        }
        if(Input.GetMouseButtonUp(0)) {
            horn.Pause();
        }
    }
}
