using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HoldDisplay : MonoBehaviour, IUICmponent
{
    [SerializeField] private TMP_Text _text;
    private readonly Vector3 _tagetSize = new Vector3(1.1f, 1.1f, 1.1f);
    private readonly float _time = 0.5f;

    public void Show()
    {
        gameObject.SetActive(true);
        LeanTween.alpha(GetComponent<RectTransform>(), 1f, 0);
        LeanTween.LeanAlphaText(_text, 1, 0);
        transform.localScale = Vector3.one;
        LeanTween.cancel(gameObject);


        LeanTween.scale(this.gameObject, _tagetSize, _time).setEase(LeanTweenType.animationCurve).setLoopPingPong();
    }

    public void Hide()
    {
        LeanTween.alpha(GetComponent<RectTransform>(), 0f, _time);
        LeanTween.LeanAlphaText(_text, 0, _time);
        gameObject.SetActive(false);
    }

}
