using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Condition), editorForChildClasses: true)]
public class ConditionEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUI.enabled = Application.isPlaying;

        Condition targetObject = target as Condition;
        if (GUILayout.Button("Reset"))
            targetObject.Reset();
        if (GUILayout.Button("Check Condition"))
            targetObject.Check();
    }
}