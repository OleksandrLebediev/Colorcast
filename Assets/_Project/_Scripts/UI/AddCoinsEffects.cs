using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AddCoinsEffects : MonoBehaviour
{
    [SerializeField] private Coin coinTemplate;
    [SerializeField] private RectTransform _target;
    [SerializeField] private RectTransform _startPosition;
    [SerializeField] private VictoryScreen _victoryScreen;

    private readonly float _radius = 150f;
    private readonly int _minCountCoins = 5;
    private readonly int _maxCountCoins = 10;
    private readonly float _speed = 10;
    private readonly float _minDistance = 0.1f;
    private readonly float _secondsWait = 0;
    private readonly float _secondsWaitBetweenCoins = 0.01f;

    private List<Coin> _coins = new List<Coin>();
    private int _countCoins;
    private int _currentCountCoints;
    private Vector3 _targetWordPosition;
    private Vector3 _startWordPosition;
    private Coroutine _smoothIncreaseCorutine;

    public event UnityAction AddCoinsAnimationEnded;

    private void OnEnable()
    {
        _victoryScreen.ClickNoThanksButton += SpawnCoinAlongRadius;
    }

    private void OnDisable()
    {
        _victoryScreen.ClickNoThanksButton -= SpawnCoinAlongRadius;
    }

    public void IncreaseAnimation(int currentValue, int targetValue, TMP_Text input, UnityAction animationEnded)
    {
        _smoothIncreaseCorutine =  StartCoroutine(SmoothIncrease(currentValue, targetValue, input, animationEnded));
    }

    public void StopIncreaseAnimation()
    {
        if (_smoothIncreaseCorutine != null)
            StopCoroutine(_smoothIncreaseCorutine);
    }

    private void SpawnCoinAlongRadius()
    {
        _countCoins = Random.Range(_minCountCoins, _maxCountCoins);
        _targetWordPosition = _target.transform.position;
        _startWordPosition = _startPosition.transform.position;
        _coins.Clear();

        for (int i = 0; i < _countCoins; i++)
        {
            Coin coin = Instantiate(coinTemplate, _startWordPosition, Quaternion.identity, transform);
            Vector3 targetPosition = _startWordPosition + (Vector3)Random.insideUnitCircle * _radius;
            StartCoroutine(CoinMovement(coin.transform, targetPosition, _speed, MoveCoinsForCoinsDisplay));
            _coins.Add(coin);
        }
    }

    private void OnAddAnimationEnded()
    {
        foreach (var coinImage in _coins)
        {
            Destroy(coinImage.gameObject);
        }
        AddCoinsAnimationEnded?.Invoke();
    }

    private void CoinsMovementCompleted(UnityAction allCoinsCompleted)
    {
        _currentCountCoints++;
        if(_currentCountCoints >= _countCoins)
        {
            _currentCountCoints = 0;
            allCoinsCompleted();
        }
    }

    private void MoveCoinsForCoinsDisplay()
    {
        StartCoroutine(WaitMoveCointsForCoinsDisplay(_secondsWait));
    }

    private IEnumerator CoinMovement(Transform coin, Vector3 target, float speed, UnityAction allCoinsCompleted)
    {
        while (true)
        {
            coin.position = Vector3.Lerp(coin.position, target, Time.deltaTime * _speed);
            if (Vector3.Distance(coin.position, target) <= _minDistance)
            {
                CoinsMovementCompleted(allCoinsCompleted);
                yield break;
            }
            yield return null;
        }
    }

    private IEnumerator WaitMoveCointsForCoinsDisplay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        for (int i = 0; i < _countCoins; i++)
        {
            yield return new WaitForSeconds(_secondsWaitBetweenCoins);
            StartCoroutine(CoinMovement(_coins[i].transform, _targetWordPosition, _speed, OnAddAnimationEnded));
        }
    }

    private IEnumerator SmoothIncrease(int currentValue, int targetValue, TMP_Text input, UnityAction animationEnded)
    {
        while (currentValue < targetValue)
        {
            yield return new WaitForSeconds(0.005f);
            currentValue++;
            input.text = currentValue.ToString();
            yield return null;
        }
        animationEnded?.Invoke();
    }
}