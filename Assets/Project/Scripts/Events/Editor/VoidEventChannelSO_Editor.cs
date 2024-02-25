using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(VoidEventChannelSO), editorForChildClasses: true)]
public class VoidEventChannelSO_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUI.enabled = Application.isPlaying;

        VoidEventChannelSO targetObject = target as VoidEventChannelSO;

        if (GUILayout.Button("RaiseEvent"))
            targetObject.RaiseEvent();
    }
}