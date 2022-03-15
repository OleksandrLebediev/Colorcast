using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class LevelsSelectPanel : MonoBehaviour
{
    [SerializeField] private PictureDispenser _pictureDispenser;
    [SerializeField] private LevelItem _levelItemTemplate;
    [SerializeField] private Transform _itemContainer;

    private LevelsItemSaveSystem _binarySaveLevelsSystem;
    private PlayerPrefsSystem _playerPrefsSystem;
    private LevelItem _previousSelectLevel;

    private void Start()
    {
        int numberLevels = _pictureDispenser.Pictures.Count();
        _binarySaveLevelsSystem = new LevelsItemSaveSystem();

        foreach (var picture in _pictureDispenser.Pictures)
        {
            Level level = _binarySaveLevelsSystem.GetLevelDate(picture.ID);
            AddLevelItem(level);
        }
    }

    private void AddLevelItem(Level level)
    {
        LevelItem levelItem = Instantiate(_levelItemTemplate, _itemContainer);
        levelItem.SelectButtonClick += OnSelectButtonClick;
        levelItem.ChangeLevel += UpdateSaveStateItem;
        levelItem.SetLevel(level);

        if (level.isSelected == true)
        {
            _previousSelectLevel = levelItem;
        }
    }

    private void OnSelectButtonClick(Level level, LevelItem levelItem)
    {
        _pictureDispenser.DispencePicture(level.Number);

        _previousSelectLevel.ChagneStateLevelSelected();
        levelItem.ChagneStateLevelSelected();
        _previousSelectLevel = levelItem;

        gameObject.SetActive(false);
    }

    private void UpdateSaveStateItem(Level level)
    {
        _binarySaveLevelsSystem.Save(level, level.Number);
    }

}
 