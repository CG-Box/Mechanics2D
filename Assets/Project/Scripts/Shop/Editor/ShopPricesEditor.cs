using UnityEditor;
using UnityEngine;

[CustomEditor(typeof( ShopPrices), editorForChildClasses: true)]
public class ShopPricesEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var targetObject = target as ShopPrices;

        if (GUILayout.Button("Init Default"))
        {
            targetObject.Init();
        }

        base.OnInspectorGUI();
    }
}

