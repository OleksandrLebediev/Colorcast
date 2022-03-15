using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameAnalyticsEventManager : MonoBehaviour
{
    [SerializeField] private LevelBuilder _levelBuilder;
    [SerializeField] private PlayerWallet _playerWallet;


    private void OnEnable()
    {
        _levelBuilder.LevelStart += OnLevelStart;
        _levelBuilder.LevelComplete += OnLevelComplete;
        _levelBuilder.LevelFail += OnLevelFail;
        //_levelBuilder.LevelFail += OnLevelRestart;
        //_playerWallet.AmountCoinsIncreased += OnAmountCoinsIncreased;
        //_playerWallet.AmountCoinsDecreased += OnAmountCoinsDecreased;
    }

    private void OnDisable()
    {
        _levelBuilder.LevelStart -= OnLevelStart;
        _levelBuilder.LevelComplete -= OnLevelComplete;
        _levelBuilder.LevelFail -= OnLevelFail;
        //_levelBuilder.LevelFail -= OnLevelRestart;
        //_playerWallet.AmountCoinsIncreased -= OnAmountCoinsIncreased;
        //_playerWallet.AmountCoinsDecreased -= OnAmountCoinsDecreased;
    }

    private void OnLevelStart(int level, LevelData levelData)
    {
        //TinySauce.OnGameStarted(levelNumber: level.ToString());
    }

    private void OnLevelComplete(LevelData levelData, LevelRewardData rewardData)
    {
        //TinySauce.OnGameFinished(true, rewardData.Starts, levelNumber: rewardData.Level.ToString());
    }

    private void OnLevelFail(int level, LevelData levelData)
    {
       // TinySauce.OnGameFinished(false,0 ,levelNumber: level.ToString());
    }

    //private void OnLevelRestart(int level, LevelData levelData)
    //{
    //    //GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, $"Level - {level}");
    //    LionAnalytics.LevelRestart(level, levelData.AttemptNumber);
    //    Debug.Log("LevelComplete", this);
    //}

    //private void OnAmountCoinsIncreased(int coins)
    //{
    //    GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, "Coins", coins, "Coins", "LevelComplete");
    //}

    //private void OnAmountCoinsDecreased(int coins)
    //{
    //    GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "Coins", coins, "Coins", "BuyPaintbrush");
    //}
}
