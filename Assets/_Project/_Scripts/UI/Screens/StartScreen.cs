using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartScreen : MonoBehaviour
{
    [SerializeField] private IUICmponent[] _UICmponents;

    private void Awake()
    {
        _UICmponents = GetComponentsInChildren<IUICmponent>();
    }

    public void Show()
    {
        foreach (var item in _UICmponents)
        {
            item.Show();
        }
    }

    public void Hide()
    {
        foreach (var item in _UICmponents)
        {
            item.Hide();
        }
    }
}
