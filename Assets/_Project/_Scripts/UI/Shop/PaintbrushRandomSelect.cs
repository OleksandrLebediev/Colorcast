using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PaintbrushRandomSelect
{
    public PaintbrushRandomSelect(IEnumerable<ShopItemDataToDisplay> itemsData)
    {
        _closeItems = itemsData.Where(item => item.IsClose).ToList();
    }

    private List<ShopItemDataToDisplay> _closeItems;

    public List<ShopItemDataToDisplay> GetRandomPaintbrushs()
    {
        int randomNumber = Random.Range(0, _closeItems.Count);
        List<ShopItemDataToDisplay> jumbledItem = new List<ShopItemDataToDisplay>();
        foreach (var item in _closeItems)
        {
            jumbledItem.Add(item);
            _closeItems.Remove(item);
        }
        return jumbledItem;
    }
}
