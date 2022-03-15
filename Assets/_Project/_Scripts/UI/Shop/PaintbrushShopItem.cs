using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PaintbrushShopItem : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Button _selectButton;
    [SerializeField] private GameObject _selected;
    [SerializeField] private GameObject _closePanel;

    private ShopItemDataToDisplay _dataToDisplay;

    public bool isClose => _dataToDisplay.IsClose;

    public event UnityAction<PaintbrushShopItem, ShopItemDataToDisplay> SelectButtonClick;
    public event UnityAction<ShopItemDataToDisplay> ChangeItemData;

    private void OnEnable()
    {
        _selectButton.onClick.AddListener(OnSelectButtonClick);
    }

    private void OnDisable()
    {
        _selectButton.onClick.RemoveListener(OnSelectButtonClick);
    }

    public void SetShopItemDate(ShopItemDataToDisplay dataToDisplay)
    {
        _dataToDisplay = dataToDisplay;
        RenderPaintbrush(dataToDisplay);
    }

    public void ChagneStateSelected(bool selected)
    {
        _dataToDisplay.ChagneStateSelected(selected);
        RenderPaintbrush(_dataToDisplay);
        ChangeItemData?.Invoke(_dataToDisplay);
    }

    public void Open()
    {
        _dataToDisplay.Open();
        OnSelectButtonClick();
    }

    private void RenderPaintbrush(ShopItemDataToDisplay dataToDisplay)
    {
        _icon.sprite = dataToDisplay.Paintbrush.Icon;
        _closePanel.SetActive(dataToDisplay.IsClose);
        _selected.SetActive(dataToDisplay.IsSelected);
    }

    private bool ChackCloseItem(ShopItemDataToDisplay dataToDisplay)
    {
        return dataToDisplay.IsClose;
    }

    public void OnSelectButtonClick()
    {
        if (ChackCloseItem(_dataToDisplay) == true)
        {
            Debug.Log("Предмет недоступен");
        }
        else
        {
            SelectButtonClick?.Invoke(this, _dataToDisplay);
        }
    }
}
