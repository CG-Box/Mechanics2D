using UnityEditor;
using UnityEngine;

[CustomEditor(typeof( GameManagerSO), editorForChildClasses: true)]
public class GameManagerSO_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUI.enabled = Application.isPlaying;

        GameManagerSO targetObject = target as GameManagerSO;

        foreach(ItemType iType in System.Enum.GetValues(typeof(ItemType)))
        {
            GUILayout.EndVertical();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Add item "+iType))
                targetObject.AddItemToPlayer(iType);
            if (GUILayout.Button("Remove item "+iType))
                targetObject.RemoveItemFromPlayer(iType);
            GUILayout.EndHorizontal();
            GUILayout.BeginVertical();
        }
    }
}
