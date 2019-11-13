using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {

    public Color nextWorldColor;
    public Color nextTreeWood;

    [SerializeField] private Renderer[] trees;
    [SerializeField] private Renderer[] treesWood;
    [SerializeField] private float speed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float distance;

    private MapColorsScriptObj[] mapColorsScriptObj;
    private Renderer renderer;
    private int loadNextColor;
    private float timer;
    private float speedAddon = 0.1f;

    private void Start() {
        NotificationCenter.OnNextLevelEvent += LevelUpHandler;
        renderer = GetComponent<Renderer>();
        nextWorldColor = renderer.material.color;
        nextTreeWood = treesWood[0].material.color;
        loadNextColor = ScoreManager.instance.currentLevel - 1;
        mapColorsScriptObj = Resources.LoadAll<MapColorsScriptObj>( "ScriptableObjects/" );
    }

    void Update() {
        WorldMovement();
        ChangeColorForSingleItem( renderer , nextWorldColor );
        ChangeColorForArrayItem( trees , nextWorldColor );
        ChangeColorForArrayItem( treesWood , nextTreeWood );
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
        if(loadNextColor >= mapColorsScriptObj.Length) {
            loadNextColor = 1;
        }
        nextWorldColor = mapColorsScriptObj[loadNextColor].mapColor;
        nextTreeWood = mapColorsScriptObj[loadNextColor].treeWoodColor;
        speed += speedAddon;
    }

    private void ChangeColorForArrayItem(Renderer[] renderObject , Color colorItem) {
        for(int i = 0; i < renderObject.Length; i++) {
            renderObject[i].material.color = Color.Lerp( renderObject[i].material.color , colorItem , Time.deltaTime * 1 );
        }
    }

    private void ChangeColorForSingleItem(Renderer renderObject , Color colorItem) {
        renderObject.material.color = Color.Lerp( renderObject.material.color , colorItem , Time.deltaTime * 1 );
    }

    private void OnDestroy() {
        NotificationCenter.OnNextLevelEvent -= LevelUpHandler;
    }
}