using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SlotManager), editorForChildClasses: true)]
public class SlotManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUI.enabled = Application.isPlaying;

        SlotManager targetObject = target as SlotManager;

        if (GUILayout.Button("Load Recent Slot"))
            targetObject.LoadRecentSlot();

        GUILayout.EndVertical();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Load Data"))
            targetObject.LoadActiveSlot();
        if (GUILayout.Button("Save Data"))
            targetObject.SaveActiveSlot();
        GUILayout.EndHorizontal();
        GUILayout.BeginVertical();
    }
}