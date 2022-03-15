using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class FirstClickHandler : MonoBehaviour
{
    [SerializeField] private HolderZone _holderZone;

    private bool _isClicked;
    public event UnityAction FirstClick;

    private void OnEnable()
    {
        _holderZone.Hold += OnFirstClick;
    }

    private void OnDisable()
    {
        _holderZone.Hold -= OnFirstClick;
    }

    private void OnFirstClick()
    {
        if (_isClicked == false)
        {
            FirstClick?.Invoke();
            _isClicked = true;
        }
    }

    public void ResetClick()
    {
        _isClicked = false;
    }
}
