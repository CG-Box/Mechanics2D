using UnityEngine;
using System;

public enum ItemType 
{
    Jam,
    Pie,
    Cupkake,
    Pizza,
    Taco,
    Kat,
    Key_red,
    Key_blue,
    Key_green,
    Key_yellow,
}

[Serializable]
public struct ItemData
{
    public ItemType type;
    public Sprite sprite;
    public bool canStack;
    public int amount;

    public ItemData(ItemType type)
    {
        this.type = type;
        this.sprite = null;
        this.canStack = false;
        this.amount = 1; 
    }
    public ItemData(ItemType type, int amount)
    {
        this.type = type;
        this.sprite = null;
        this.canStack = true;
        this.amount = amount; 
    }
    public ItemData(ItemType type, Sprite sprite)
    {
        this.type = type;
        this.sprite = sprite;
        this.canStack = false;
        this.amount = 1; 
    }
    public ItemData(ItemType type, Sprite sprite, bool canStack)
    {
        this.type = type;
        this.sprite = sprite;
        this.canStack = canStack;
        this.amount = 1; 
    }
    public ItemData(ItemType type, Sprite sprite, bool canStack, int amount)
    {
        this.type = type;
        this.sprite = sprite;
        this.canStack = canStack;
        this.amount = amount;
    }

    public void PrintToLog()
    {
        Debug.Log($"ItemType: {type}, canStack: {canStack}, amount: {amount}, sprite: {sprite.name}");
    }
}