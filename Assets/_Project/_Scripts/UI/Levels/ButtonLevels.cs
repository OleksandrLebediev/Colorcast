using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLevels : MonoBehaviour
{
    [SerializeField] private LevelsSelectPanel _levels;
    [SerializeField] private Button _openButton;
    [SerializeField] private Button _closeButton;

    private void OnEnable()
    {
        _openButton.onClick.AddListener(OpenLevels);
        _closeButton.onClick.AddListener(CloseLevels);
    }

    private void OnDisable()
    {
        _openButton.onClick.RemoveListener(OpenLevels);
        _closeButton.onClick.RemoveListener(CloseLevels);
    }

    private void OpenLevels()
    {
        _levels.gameObject.SetActive(true);
    }

    private void CloseLevels()
    {
        _levels.gameObject.SetActive(false);
    }

}
