using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameData_SO), editorForChildClasses: true)]
public class GameDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUI.enabled = Application.isPlaying;

        GameData_SO targetObject = target as GameData_SO;
        if (GUILayout.Button("Reset (Game restart needed)"))
            targetObject.ResetToDefault();


        int i = 0;
        foreach (string name in System.Enum.GetNames(typeof(ItemType)))
        {
            GUILayout.EndVertical();
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Get Item : "+name))
                targetObject.OwnInventoryItem((ItemType)i);
            if (GUILayout.Button("Remove Item : "+name))
                targetObject.LoseInventoryItem((ItemType)i);

            i++;

            GUILayout.EndHorizontal();
            GUILayout.BeginVertical();
        }
    }
}