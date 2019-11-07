using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeHead : MonoBehaviour
{
    [SerializeField] private World world;
    [SerializeField] private Renderer treeColor;

    private void Start() {
       NotificationCenter.OnNextLevelEvent += LevelUp;

        treeColor = GetComponent<Renderer>();
        world = GetComponentInParent<World>();
    }

    private void LevelUp() {
        treeColor.material.color = world.nextWorldColor;
    }

    private void OnDestroy() {
        NotificationCenter.OnNextLevelEvent -= LevelUp;
    }
}
