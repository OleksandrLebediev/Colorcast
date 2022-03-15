using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PaintbrushDispenser _paintbrushDispenser;
    [SerializeField] private PaintbrushShopItem _paintbrushShopItemTemplate;
    [SerializeField] private ShopItemSaveSystem _shopItemSaveSystem;
    [SerializeField] private Transform _content;
    [SerializeField] private Button _paitbrushBuyButton;
    [SerializeField] private Image _paintbrushPerformance;

    private List<PaintbrushShopItem> _paintbrushShopItems;
    private PaintbrushShopItem _currentSelectedItem;
    private readonly int _paintbrushPrice = 1000;

    private void OnEnable()
    {
        _paitbrushBuyButton.onClick.AddListener(TryBuyPaitbrush);
    }

    private void OnDisable()
    {
        _paitbrushBuyButton.onClick.RemoveListener(TryBuyPaitbrush);
    }

    private void OnDestroy()
    {
        foreach (var item in _paintbrushShopItems)
        {
            item.SelectButtonClick -= OnSelectButtonClick;
            item.ChangeItemData -= UpdateItemDataToDisplay;
        }
    }

    private void Start()
    {
        _shopItemSaveSystem = new ShopItemSaveSystem();
        _paintbrushShopItems = new List<PaintbrushShopItem>();

        foreach (var paintbrush in _paintbrushDispenser.Paintbrushes)
        {
            ItemData data = _shopItemSaveSystem.GetShopItemDate(paintbrush);
            ShopItemDataToDisplay dataToDisplay = new ShopItemDataToDisplay(paintbrush, data);
            AddPaintbrushItem(dataToDisplay);
        }    
    }

    private void AddPaintbrushItem(ShopItemDataToDisplay dataToDisplay)
    {
        PaintbrushShopItem shopItem = Instantiate(_paintbrushShopItemTemplate, _content);
        InitializeItem(shopItem, dataToDisplay);
    }

    private void InitializeItem(PaintbrushShopItem item, ShopItemDataToDisplay dataToDisplay)
    {
        item.SetShopItemDate(dataToDisplay);
        item.SelectButtonClick += OnSelectButtonClick;
        item.ChangeItemData += UpdateItemDataToDisplay;
        _paintbrushShopItems.Add(item);

        if(dataToDisplay.IsSelected == true)
        {
            _currentSelectedItem = item;
            _paintbrushPerformance.sprite = dataToDisplay.Paintbrush.Icon;
        }
    }

    private void UpdateItemDataToDisplay(ShopItemDataToDisplay dataToDisplay)
    {
        _shopItemSaveSystem.Save(dataToDisplay.ItemData, dataToDisplay.Paintbrush.ID);
    }

    private void OnSelectButtonClick(PaintbrushShopItem item, ShopItemDataToDisplay dataToDisplay)
    {
        if(dataToDisplay.IsSelected == true)
        {
            return;
        }

        item.ChagneStateSelected(true);
        _currentSelectedItem.ChagneStateSelected(false);
        _currentSelectedItem = item;
        _paintbrushPerformance.sprite = dataToDisplay.Paintbrush.Icon;
        _paintbrushDispenser.DispencePaintbrush(dataToDisplay.Paintbrush);
    }

    private void TryBuyPaitbrush()
    {
        if(_player.ChackSolvency(_paintbrushPrice) == true)
        {
            _player.WithdraMoney(_paintbrushPrice);
            List<PaintbrushShopItem> closeShopItems = _paintbrushShopItems.Where(item => item.isClose == true).ToList();

            if (closeShopItems.Count == 0)
            {
                return;
            }

            int randomNumber = Random.Range(0, closeShopItems.Count);
            PaintbrushShopItem randomItem = closeShopItems[randomNumber];
            closeShopItems.Remove(randomItem);
            randomItem.Open();
        }
    }
}
