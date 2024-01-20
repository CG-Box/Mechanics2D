using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameData_SO), editorForChildClasses: true)]
public class GameDataEditor : Editor
{
    private enum items { Sam, Kate, Alice };

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUI.enabled = Application.isPlaying;

        GameData_SO targetObject = target as GameData_SO;
        if (GUILayout.Button("Reset (Game restart needed)"))
            targetObject.ResetToDefault();

        foreach (string name in System.Enum.GetNames(typeof(items)))
        {
            GUILayout.EndVertical();
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Get Item : "+name))
                targetObject.OwnInventoryItem(name);
            if (GUILayout.Button("Remove Item : "+name))
                targetObject.LoseInventoryItem(name);

            GUILayout.EndHorizontal();
            GUILayout.BeginVertical();
        }
    }
}