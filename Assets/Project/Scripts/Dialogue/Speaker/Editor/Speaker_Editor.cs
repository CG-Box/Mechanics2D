using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Speaker), editorForChildClasses: true)]
public class Speaker_Editor : Editor
{
    Speaker targetClass;

    void OnEnable()
    {
        targetClass = target as Speaker;
    }

    public override void OnInspectorGUI()
    {
        DrawSpriteLabel();

        base.OnInspectorGUI();
    }

    void DrawSpriteLabel()
    {
        //Prevent errors
        if (targetClass.emotionsList == null || targetClass.emotionsList.Length == 0 || targetClass.emotionsList[0].image == null)
            return;
            
        //Convert the sprite (see SO script) to Texture
        Texture2D texture = AssetPreview.GetAssetPreview(targetClass.emotionsList[0].image);
        //We crate empty space 80x80 (you may need to tweak it to scale better your sprite
        //This allows us to place the image JUST UNDER our default inspector
        GUILayout.Label("", GUILayout.Height(80), GUILayout.Width(80));
        //Draws the texture where we have defined our Label (empty space)
        GUI.DrawTexture(GUILayoutUtility.GetLastRect(), texture);
    }
}