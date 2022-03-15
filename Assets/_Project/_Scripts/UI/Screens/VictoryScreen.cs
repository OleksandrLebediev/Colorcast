using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(VictoryScreenAnimation))]
public class VictoryScreen : MonoBehaviour
{
    [SerializeField] private Button _buttonNoThanks;

    private readonly float _delayTimeNextButton = 3f;
    private readonly float _delayTimeProfit = 0f;
    private int _amountStars = 0;
    private VictoryScreenAnimation _animation;

    public event UnityAction ClickNoThanksButton;
    public event UnityAction VictoryScreenHide;
    public event UnityAction VictoryScreenShow;

    private void OnEnable()
    {
        _buttonNoThanks.onClick.AddListener(OnClickNoThanksButton);
    }

    private void OnDisable()
    {
        _buttonNoThanks.onClick.RemoveListener(OnClickNoThanksButton);
    }

    private void Awake()
    {
        _animation = GetComponent<VictoryScreenAnimation>();
    }

    public void Init(int amountStars)
    {
        _amountStars = amountStars;
        _buttonNoThanks.interactable = true;
    }

    public void Show()
    {
        gameObject.SetActive(true);
        _animation.ShowAnimationStars(_amountStars);
        _animation.ShowAdsButton();
        _animation.ShowProfit(_delayTimeProfit);
        _animation.ShowNextButton(_delayTimeNextButton);
        VictoryScreenShow?.Invoke();
    }

    public void Hide()
    {
        _animation.RebindAllWinAnimations();
        gameObject.SetActive(false);
    }

    public void OnClickNoThanksButton()
    {
        _buttonNoThanks.interactable = false;
        ClickNoThanksButton?.Invoke();
    }
}
