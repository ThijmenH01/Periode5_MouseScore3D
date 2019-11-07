using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTillStart : MonoBehaviour {

    [SerializeField] private Animator animator;

    private void Start() {
        GameManager.instance.OnGameStartEvent += MoveCountDownAway;

        animator = GetComponent<Animator>();
    }

    private void MoveCountDownAway() {
        animator.SetTrigger( "StartPlay" );
    }

    public void DestroyText() {
        gameObject.SetActive( false );
    }

    private void OnDestroy() {
        GameManager.instance.OnGameStartEvent -= MoveCountDownAway;
    }
}
