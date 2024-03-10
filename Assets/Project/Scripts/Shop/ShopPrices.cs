using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public struct TypedPrice 
{
    public ItemType type;
    public int Value;
}

[CreateAssetMenu(fileName = "NewShopPrices", menuName = "ScriptableObjects/Shop/Prices", order = 99)]
public class ShopPrices : ScriptableObject
{
    [SerializeField] TypedPrice[] pricesArray;

    Dictionary<ItemType, int> prices = default;

    public int this[ItemType itemType]
    {
        get
        {
            if (prices.TryGetValue(itemType, out int price))
            {
                return price;
            }
            else
            {
                Debug.LogError($"Wrong ItemType : {itemType}");
                return 0;
            }
        }
    }

    [ContextMenu("Init")]
    public void Init()
    {
        GenerateList();
        ToDictionary();
    }

    public void GenerateList()
    {
        pricesArray  = new TypedPrice[System.Enum.GetValues(typeof(ItemType)).Length];
        int index = 0;
        foreach(ItemType iType in System.Enum.GetValues(typeof(ItemType)))
        {
            pricesArray[index].type = iType;
            pricesArray[index].Value = 1;
            index++;
        }
    }
    
    public void ToDictionary()
    {
        prices = new Dictionary<ItemType, int>();
        foreach(TypedPrice typedPrice in pricesArray)
        {
            prices[typedPrice.type] = typedPrice.Value;
        }
    }

    void OnValidate()
    {
        ToDictionary();
    }
}
