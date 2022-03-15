using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonShop : MonoBehaviour
{
    [SerializeField] private Shop _shop;
    [SerializeField] private Button _openShop;
    [SerializeField] private Button _closeShop;

    private void OnEnable()
    {
        _openShop.onClick.AddListener(OpenShop);
        _closeShop.onClick.AddListener(CloseShop);
    }

    private void OnDisable()
    {
        _openShop.onClick.RemoveListener(OpenShop);
        _closeShop.onClick.RemoveListener(CloseShop);
    }

    private void OpenShop()
    {
        _shop.gameObject.SetActive(true);
    }

    private void CloseShop()
    {
        _shop.gameObject.SetActive(false);
    }

}
