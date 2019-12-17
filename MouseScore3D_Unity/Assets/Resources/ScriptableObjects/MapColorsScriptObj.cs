using UnityEngine;

[CreateAssetMenu( fileName = "New Map Color" , menuName = "Map Colors/New Map Color")]
public class MapColorsScriptObj : ScriptableObject {
    public Color mapColor;
    public Color treeWoodColor;
    public Color backgroundColor;
}
