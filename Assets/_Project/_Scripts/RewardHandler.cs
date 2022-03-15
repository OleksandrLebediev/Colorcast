using UnityEngine;
using UnityEngine.Events;

public class RewardHandler : MonoBehaviour
{
    [SerializeField] private PicturePlace _picturePlace;
    
    private readonly int _rate = 100;
    private LevelRewardData _reward;

    public event UnityAction<LevelRewardData> LevelRewardCalculated;
    public event UnityAction<LevelRewardData> LevelRewardReceived;

    private void OnEnable()
    {
        _picturePlace.PictureComplete += ÑalculateLevelReward;
    }

    private void OnDisable()
    {
        _picturePlace.PictureComplete -= ÑalculateLevelReward;
    }

    private void ÑalculateLevelReward(IPictureData picture)
    {
        _reward = new LevelRewardData(picture.ID, picture.Health, picture.Health * _rate);
        LevelRewardCalculated?.Invoke(_reward);
    }

    public int GetRewardCoins()
    {
        LevelRewardReceived?.Invoke(_reward);
        return _reward.CountCoins;
    }

    public int GetRewardCoins(int modified)
    {
        LevelRewardData reward = new LevelRewardData(_reward.Level, _reward.Starts, _reward.CountCoins * modified);
        LevelRewardReceived?.Invoke(_reward);
        return reward.CountCoins;
    }
}

public class LevelRewardData
{
    public LevelRewardData(int level, int starts ,int countCoins)
    {
        Level = level;
        Starts = starts;
        CountCoins = countCoins;
    }

    public int Level { get; private set; }
    public int Starts { get; private set; }
    public int CountCoins { get; private set; }
}
