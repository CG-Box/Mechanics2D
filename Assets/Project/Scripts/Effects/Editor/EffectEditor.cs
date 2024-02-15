using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Effect), editorForChildClasses: true)]
public class EffectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUI.enabled = Application.isPlaying;

        Effect targetObject = target as Effect;
        if (GUILayout.Button("Apply Effect"))
            targetObject.Apply();
    }
}