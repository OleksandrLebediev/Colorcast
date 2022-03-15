using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System.Threading.Tasks;

public class LossScreen : MonoBehaviour
{
    [SerializeField] private GameObject _warning;
    [SerializeField] private TMP_Text _text;

    private readonly float _time = 0.5f;
    private readonly float _waitTime = 0.5f;
    private readonly Vector3 _tagetSize = Vector3.one;

    public event UnityAction LossScreenHide;

    public async void Show()
    {
        _warning.transform.localScale = Vector3.zero;
        gameObject.SetActive(true);

        LeanTween.alpha(_warning.GetComponent<RectTransform>(), 1f, 0);
        LeanTween.LeanAlphaText(_text, 1, 0);
        LeanTween.cancel(_warning);

        LeanTween.scale(_warning, _tagetSize, _time).setEase(LeanTweenType.animationCurve);
        await Task.Delay(1500);
        LeanTween.alpha(_warning.GetComponent<RectTransform>(), 0f, _time).setOnComplete(() => LossScreenHide?.Invoke());
        LeanTween.LeanAlphaText(_text, 0, _time);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
