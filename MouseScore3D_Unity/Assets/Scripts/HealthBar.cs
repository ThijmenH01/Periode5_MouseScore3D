using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {
    [SerializeField] private Player player;
    [SerializeField] private Transform healthBar;
    [SerializeField] private Animator animator;

    private void Start() {
        Player.OnOffroad += ShakeBarHandler;
        NotificationCenter.OnGameOverEvent += GameOverHandler;
    }

    public void ShakeBarHandler(bool allowedToShake) {
        animator.SetBool( "Shaking" , allowedToShake );
        SetSize( player.health / 100 );
    }

    private void GameOverHandler() {
        gameObject.SetActive( false );
    }

    public void SetSize(float sizeNormalized) {
        healthBar.localScale = new Vector2( 1f , sizeNormalized );
    }

    private void OnDestroy() {
        Player.OnOffroad -= ShakeBarHandler;
        NotificationCenter.OnGameOverEvent -= GameOverHandler;
    }
}
