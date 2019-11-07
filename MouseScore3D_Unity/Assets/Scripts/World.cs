using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {
    
    public Color nextWorldColor;

    [SerializeField] private MapColorsScriptObj[] mapColorsScriptObj;
    [SerializeField] private Renderer[] trees;
    [SerializeField] private float speed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float distance;

    private Renderer renderer;
    private int loadNextColor;
    private float timer;
    private float speedAddon = 0.1f;

    private void Start() {
        NotificationCenter.OnNextLevelEvent += LevelUpHandler;

        renderer = GetComponent<Renderer>();

        nextWorldColor = renderer.material.color;
        loadNextColor = ScoreManager.instance.currentLevel;
    }

    void Update() {
        WorldMovement();
        
        renderer.material.color = Color.Lerp( renderer.material.color , nextWorldColor , Time.deltaTime * 1 );
        for(int i = 0; i < trees.Length; i++) {
            trees[i].material.color = Color.Lerp( renderer.material.color , nextWorldColor , Time.deltaTime * 1 );
        }
    }

    private void WorldMovement() {
        transform.Rotate( 0 , rotateSpeed * Time.deltaTime , 0 );
        timer += Time.deltaTime * speed;
        Vector3 _newPos = transform.position;
        _newPos.x = (Mathf.PerlinNoise( timer , timer ) - 0.5f) * distance;
        transform.position = _newPos;
    }

    private void LevelUpHandler() {
        loadNextColor++;
        nextWorldColor = mapColorsScriptObj[loadNextColor].mapColor;
        speed += speedAddon;
        if(loadNextColor >= mapColorsScriptObj.Length - 1) {
            loadNextColor = 1;
        }
    }

    private void OnDestroy() {
        NotificationCenter.OnNextLevelEvent -= LevelUpHandler;
    }
}