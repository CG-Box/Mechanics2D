using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SpeakersLibrary), editorForChildClasses: true)]
public class SpeakersLibrary_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SpeakersLibrary database = target as SpeakersLibrary;
        if (GUILayout.Button("GenerateDatabase"))
            database.CollectItemsFromFolder();
    }
}