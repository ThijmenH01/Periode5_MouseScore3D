using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private void Update() {
        transform.Translate( moveSpeed * Time.deltaTime , 0 , 0 );
    }
}
