using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationCenter : MonoBehaviour
{

    public delegate void OnNextLevelAction();
    public static event OnNextLevelAction OnNextLevel;

    public static void NextLevelReached() {
        OnNextLevel?.Invoke();
    }
}
