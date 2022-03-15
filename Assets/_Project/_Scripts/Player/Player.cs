using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerWallet _playerWallet;
    [SerializeField] private VictoryScreen _victoryScreen;
    [SerializeField] private RewardHandler _profitHandler;

    private void OnEnable()
    {
        _victoryScreen.ClickNoThanksButton += DepositMoneyForPicture;
    }


    private void OnDisable()
    {
        _victoryScreen.ClickNoThanksButton -= DepositMoneyForPicture;
    }

    public bool ChackSolvency(int price)
    {
        return _playerWallet.Coins >= price;
    }

    public void WithdraMoney(int price)
    {
        _playerWallet.WithdrawCoins(price);
    }

    public void BuyPaintbrush(Paintbrush paintbrush)
    {

    }

    private void DepositMoneyForPicture()
    {
        int profit = _profitHandler.GetRewardCoins();
        DepositMoneyForPicture(profit);
    }

    private void DepositMoneyForPicture(int amount)
    {
        _playerWallet.AddCoins(amount);
    }
}
