using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemSaveSystem : BinarySaveSystem<ItemData>
{
    private readonly string _saveFileName = "item";
    private readonly string _saveDirectoryName = "Shop";

    public ShopItemSaveSystem()
    {
        SetPath(_saveFileName, _saveDirectoryName);
    }

    public ItemData GetShopItemDate(Paintbrush item)
    {
        ItemData data;
        int id = item.ID;
        bool isSelect = false;
        bool isClose = true;

        if (ChackExistFile(id) == false)
        {
            if (id == 0)
            {
                isSelect = true;
                isClose = false;
            }

            data = new ItemData(isClose, isSelect);
            Save(data, id);
        }
        else
        {
            data = Load(id);
        }
        return data;
    }
}
