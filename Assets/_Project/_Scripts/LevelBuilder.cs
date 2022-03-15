using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelBuilder : MonoBehaviour
{
    public const string CURRENT_LEVEL = "CurrentLevel";

    [SerializeField] private PicturePlace _picturePlace;
    [SerializeField] private RewardHandler _rewardHandler;

    private PlayerPrefsSystem _playerPrefsSystem = new PlayerPrefsSystem(CURRENT_LEVEL);
    private LevelDataSaveSystem _levelDataSaveSystem;

    public event UnityAction<LevelData, LevelRewardData> LevelComplete;
    public event UnityAction<int, LevelData> LevelStart;
    public event UnityAction<int, LevelData> LevelFail;

    private void Awake()
    {
        _levelDataSaveSystem = new LevelDataSaveSystem();
    }

    private void OnEnable()
    {
        _picturePlace.PictureStart += OnLevelStart;
        _picturePlace.PictureFail += OnLevelFail;
        _rewardHandler.LevelRewardReceived += OnLevelComplete;
    }

    private void OnDisable()
    {
        _picturePlace.PictureStart -= OnLevelStart;
        _picturePlace.PictureFail -= OnLevelFail;
        _rewardHandler.LevelRewardReceived -= OnLevelComplete;
    }

    public int GetCurrentLevel()
    {
       return  _playerPrefsSystem.Load();
    }

    public void SetCurrentLevel(int level)
    {
        _playerPrefsSystem.Save(level);
    }

    private void OnLevelStart(IPictureData picture)
    {
        LevelData levelData = _levelDataSaveSystem.GetIncreaseAttemptIncreaseAttempt(picture.ID);
        LevelStart?.Invoke(picture.ID, levelData);
    }

    private void OnLevelComplete(LevelRewardData reward)
    {
        LevelData levelData = _levelDataSaveSystem.GetIncreaseAttemptIncreaseAttempt(reward.Level);
        LevelComplete?.Invoke(levelData, reward);
    }
    
    private void OnLevelFail(IPictureData picture)
    {
        LevelData levelData = _levelDataSaveSystem.GetIncreaseAttemptIncreaseAttempt(picture.ID);
        LevelFail?.Invoke(picture.ID, levelData);
    }

}
