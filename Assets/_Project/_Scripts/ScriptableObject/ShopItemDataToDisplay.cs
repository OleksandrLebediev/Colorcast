using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class ShopItemDataToDisplay
{
    public ShopItemDataToDisplay(Paintbrush paintbrush, ItemData itemData)
    {
        Paintbrush = paintbrush;
        ItemData = itemData;
    }

    public Paintbrush Paintbrush { get; private set; }
    public bool IsSelected => ItemData.IsSelected;
    public bool IsClose => ItemData.IsClose;

    public ItemData ItemData { get; private set; }

    public void Open()
    {
        ItemData = new ItemData(false, ItemData.IsSelected);
    }

    public void ChagneStateSelected(bool value)
    {
        ItemData = new ItemData(ItemData.IsClose, value);
    }
}

[Serializable]
public class ItemData
{
    public ItemData(bool isClose, bool isSelected)
    {
        IsClose = isClose;
        IsSelected = isSelected;
    }
       
    public bool IsSelected { get; private set; }
    public bool IsClose { get; private set; }
}


