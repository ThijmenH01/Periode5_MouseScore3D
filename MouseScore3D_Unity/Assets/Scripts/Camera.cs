using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    private void LateUpdate () {
        Vector3 _desiredPos = target.position + offset;
        Vector3 _smoothPos = Vector3.Lerp ( transform.position , _desiredPos , smoothSpeed);
        transform.position = _smoothPos;
    }
}
