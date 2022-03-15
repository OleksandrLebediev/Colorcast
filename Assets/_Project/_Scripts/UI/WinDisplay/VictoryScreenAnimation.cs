using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScreenAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animatorStars;
    [SerializeField] private Animator _animatorButtonAds;
    [SerializeField] private Animator _animatorNextButton;
    [SerializeField] private Animator _animatorProfit;

    private readonly string _nameAnimationStars = "star_";
    private readonly string _nameAnimationShowAdsButton = "Show";
    private readonly string _nameAnimationNextButton = "Show";
    private readonly string _nameAnimationProfit = "ProfitShow";

    public delegate void call();

    public void ShowAnimationStars(int stars)
    {
        _animatorStars.SetTrigger(_nameAnimationStars + stars);
    }
    
    public void ShowAdsButton()
    {
        _animatorButtonAds.SetTrigger(_nameAnimationShowAdsButton);
    }

    public void ShowNextButton(float delayTime)
    {
        StartCoroutine(Wait(delayTime, 
            () => _animatorNextButton.SetTrigger(_nameAnimationNextButton)));
    }

    public void ShowProfit(float delayTime)
    {
        StartCoroutine(Wait(delayTime,
            () => _animatorProfit.SetTrigger(_nameAnimationProfit)));
    }

    public void RebindAllWinAnimations()
    {
        _animatorStars.Rebind();
        _animatorButtonAds.Rebind();
        _animatorNextButton.Rebind();
        _animatorProfit.Rebind();
    }

    private IEnumerator Wait(float delayTime, call call)
    {
        yield return new WaitForSeconds(delayTime);
        call();
    }
}
