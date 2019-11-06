using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpUI : MonoBehaviour
{
    public Text levelReachedText;

    private void DisableUI() {
        SetUIState( false );
    }

    public void SetUIState(bool setActive) {
        gameObject.SetActive( setActive );
    }
}
