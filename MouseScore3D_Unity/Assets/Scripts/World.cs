using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float distance;

    private float timer;

    void Update() {

        transform.Rotate(0,rotateSpeed * Time.deltaTime, 0);

        timer += Time.deltaTime * speed;

        Vector3 _newPos = transform.position;
        _newPos.x = (Mathf.PerlinNoise(timer, timer) - 0.5f) * distance;
        transform.position = _newPos;
    }
}