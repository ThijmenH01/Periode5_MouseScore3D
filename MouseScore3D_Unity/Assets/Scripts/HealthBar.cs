using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Transform healthBar;

    public void SetSize(float sizeNormalized) {
        healthBar.localScale = new Vector2(1f , sizeNormalized);
    }
}
