using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {

    public float rotationSpeed;
    public float roationFriction;
    public float rotationSmoothness;

    private float valueResult;
    [SerializeField] private Player player;

    private void Update() {
        //Quaternion rotateFrom;
        //Quaternion rotateTo;

        Vector3 translateFrom;
        Vector3 translateTo;

        if(GameManager.instance.gameOver) {
            translateFrom = transform.position;
            translateTo = new Vector3 (0 , 3.337f , -20);
            transform.position = Vector3.Lerp(translateFrom , translateTo , Time.deltaTime * rotationSmoothness);

            //rotateFrom = transform.rotation;
            //rotateTo = Quaternion.Euler(-60 , 0 , 0);
            //transform.rotation = Quaternion.Lerp(rotateFrom , rotateTo , Time.deltaTime * rotationSmoothness);


            if(GameManager.instance.gameOver) {
                Time.timeScale -= Time.deltaTime;
            }
        }
    }
}
