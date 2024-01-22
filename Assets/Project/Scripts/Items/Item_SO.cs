using UnityEngine;

[CreateAssetMenu(fileName = "Item_SO", menuName = "Inventory Items/Item_SO", order = 1)]
public class Item_SO: ScriptableObject
{
    //[SerializeField]private Transform CollectableTransform;

    [SerializeField]private ItemData itemData;

    public ItemData Data
    {
        get { return itemData; }
    }

    void OnEnable()
    {
        //itemData = new ItemData(type, sprite, canStack, amount);
    }
}
