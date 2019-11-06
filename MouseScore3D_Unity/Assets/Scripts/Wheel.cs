using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour {
    [SerializeField] private float rotationSpeed;

    private void Update() {
        transform.Rotate( rotationSpeed , 0 , 0 );
    }
}
