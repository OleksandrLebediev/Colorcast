using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class RewardCoinsDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _amontProfit;
    [SerializeField] private RewardHandler _rewardHandler;

    private void OnEnable()
    {
        _rewardHandler.LevelRewardCalculated += UpdateRewardCoins; 
    }

    private void OnDestroy()
    {
        _rewardHandler.LevelRewardCalculated -= UpdateRewardCoins;
    }

    private void UpdateRewardCoins(LevelRewardData reward)
    {
        _amontProfit.text = $"+{reward.CountCoins.ToString()}";
    }

}
