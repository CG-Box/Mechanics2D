using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ItemsLibrary), editorForChildClasses: true)]
public class ItemsLibrary_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ItemsLibrary database = target as ItemsLibrary;
        if (GUILayout.Button("GenerateDatabase"))
            database.CollectItemsFromFolder();
    }
}