using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerDisplay : MonoBehaviour
{
    [SerializeField] private Image _lineOfTimer;
    [SerializeField] private TMP_Text _dial;

    private readonly string SHOW = "Show";
    private readonly string HIDE = "Hide";

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        _animator.SetTrigger(SHOW);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        _animator.SetTrigger(HIDE);
    }

    public void UpdateLineOfTimer(float value)
    {
        _lineOfTimer.fillAmount = value;
    }

    public void UpdateDial(float value)
    {
        _dial.text = value.ToString();
    }
}
