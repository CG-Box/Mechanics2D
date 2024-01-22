using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemsLibrary", menuName = "Inventory Items/ItemsLibrary", order = 2)]
public class ItemsLibrary : ScriptableObject
{
    [SerializeField]
    private List<Item_SO> itemsViewList; //only for viewing in the inspector
    //private Dictionary<ItemType, Item_SO> itemsDatabase;

    static Dictionary<ItemType, Item_SO> itemsDatabase;

    [ContextMenu("GenerateDatabase")]
    public void CollectItemsFromFolder()
    {
        itemsViewList = Resources.LoadAll<Item_SO>(path: "Items").OrderBy(item => item.Data.type).ToList();

        //For static
        itemsDatabase = itemsViewList.ToDictionary(keySelector: item => item.Data.type, elementSelector: item => item);
    }

    public ItemData GetItemData(ItemType type)
    {
        return itemsViewList.Find(item => item.Data.type == type).Data;
        //return itemsDatabase[type].Data;
    }

    public static ItemData GetItem(ItemType type)
    {
        return itemsDatabase[type].Data;
    }
}
