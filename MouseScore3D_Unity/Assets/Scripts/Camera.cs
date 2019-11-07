using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {

    public float rotationSmoothness;

    private void Update() {
        Vector3 translateFrom;
        Vector3 translateTo;

        if(GameManager.instance.gameIsOver) {
            translateFrom = transform.position;
            translateTo = new Vector3 (0 , transform.position.y , -20);
            transform.position = Vector3.Lerp(translateFrom , translateTo , Time.deltaTime * rotationSmoothness);

            if(GameManager.instance.gameIsOver) {
                Time.timeScale -= Time.deltaTime;
            }
        }
    }
}
