using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {
    [SerializeField] private Player player;
    [SerializeField] private Transform healthBar;
    [SerializeField] private Animator animator;

    private void Start() {
        Player.OnOffroad += ShakeBar;
    }

    public void ShakeBar(bool allowedToShake) {
        animator.SetBool( "Shaking" , allowedToShake );
        SetSize( player.health / 100 );
    }

    public void SetSize(float sizeNormalized) {
        healthBar.localScale = new Vector2( 1f , sizeNormalized );
    }

    private void OnDestroy() {
        Player.OnOffroad -= ShakeBar;
    }
}
