using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTillStart : MonoBehaviour {

    [SerializeField] private Animator animator;

    private void Start() {
        NotificationCenter.OnGameStartEvent += MoveCountDownAwayHandler;
        animator = GetComponent<Animator>();
    }

    private void MoveCountDownAwayHandler() {
        animator.SetTrigger( "StartPlay" );
    }

    public void DestroyText() {
        gameObject.SetActive( false );
    }

    private void OnDestroy() {
        NotificationCenter.OnGameStartEvent -= MoveCountDownAwayHandler;
    }
}
