using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

[RequireComponent(typeof(AddCoinsEffects))]
public class WalletDisplay : MonoBehaviour
{
    [SerializeField] private PlayerWallet _playerWallet;
    [SerializeField] private TMP_Text _amountCoins;

    private AddCoinsEffects _coinsAnimation;
    private int _currentBalance;

    public event UnityAction AnimationBalanceChanceEnded;

    private void OnEnable()
    {
        _playerWallet.AmountCoinsIncreasedAnimation += OnBalanceChange;
        _playerWallet.AmountCoinsChange += OnBalanceChange;

    }

    private void OnDisable()
    {
        _playerWallet.AmountCoinsIncreasedAnimation -= OnBalanceChange;
        _playerWallet.AmountCoinsChange -= OnBalanceChange;
    }

    private void Awake()
    {
        _coinsAnimation = GetComponent<AddCoinsEffects>();
    }

    private void OnBalanceChange(int oldAmount, int currentAmount)
    {
        _currentBalance = currentAmount;
        _coinsAnimation.IncreaseAnimation(oldAmount, currentAmount, _amountCoins, AnimationBalanceChanceEnded);
    }

    private void OnBalanceChange(int amount)
    {
        _amountCoins.text = amount.ToString();
    }

    public void StopAnimationCoins()
    {
        _coinsAnimation.StopIncreaseAnimation();
        OnBalanceChange(_currentBalance);
    }
}
