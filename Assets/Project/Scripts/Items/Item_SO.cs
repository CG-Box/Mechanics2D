using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_SO", menuName = "ScriptableObjects/Inventory Items/Item_SO", order = 1)]
public class Item_SO: ScriptableObject
{
    //[SerializeField]private Transform CollectableTransform;

    [SerializeField]private ItemData itemData;

    //[SerializeField]private List<Effect> useEffects;
    //[SerializeField]private List<Effect> passiveEffects;

    public ItemData Data
    {
        get { return itemData; }
    }

    void OnEnable()
    {
        //itemData = new ItemData(type, sprite, canStack, amount);
    }
}
