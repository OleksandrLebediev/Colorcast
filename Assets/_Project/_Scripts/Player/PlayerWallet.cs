using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class PlayerWallet : MonoBehaviour, IWalletBallace
{
    private int _coins;
    private int _oldBalanceCoins;
    private readonly string CoinsKey = "Coins";
    private PlayerPrefsSystem _playerPrefsSystem;

    public int Coins => _coins;

    public event UnityAction<int> AmountCoinsChange;
    public event UnityAction<int> AmountCoinsIncreased;
    public event UnityAction<int> AmountCoinsDecreased;
    public event UnityAction<int, int> AmountCoinsIncreasedAnimation;

    private void Start()
    {
        _playerPrefsSystem = new PlayerPrefsSystem(CoinsKey);
        _coins = _playerPrefsSystem.Load();
        AmountCoinsChange?.Invoke(_coins);
    }

    public void AddCoins(int amount)
    {
        _oldBalanceCoins = _coins;
        _coins += amount;
        _playerPrefsSystem.Save(_coins);
        AmountCoinsChange?.Invoke(_coins);
        AmountCoinsIncreased?.Invoke(_coins);
        AmountCoinsIncreasedAnimation?.Invoke(_oldBalanceCoins, _coins);
    }

    public void WithdrawCoins(int amount)
    {
        _coins -= amount;
        _playerPrefsSystem.Save(_coins);
        AmountCoinsChange?.Invoke(_coins);
        AmountCoinsDecreased?.Invoke(_coins);
    }
}

public interface IWalletBallace
{
    public int Coins { get; }

    public event UnityAction<int, int> AmountCoinsIncreasedAnimation;
    public event UnityAction<int> AmountCoinsIncreased;
    public event UnityAction<int> AmountCoinsDecreased;
}

