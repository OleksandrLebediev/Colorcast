using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class LevelItem : MonoBehaviour
{
    [SerializeField] private TMP_Text _number;
    [SerializeField] private Image[] _stars;
    [SerializeField] private Image _select;
    [SerializeField] private Button _selectButton;

    private Level _level;

    public Level Level => _level;

    public event UnityAction<Level, LevelItem> SelectButtonClick;
    public event UnityAction<Level> ChangeLevel;

    private void OnEnable()
    {
        _selectButton.onClick.AddListener(OnSelectButtonClick);
    }

    private void OnDisable()
    {
        _selectButton.onClick.RemoveListener(OnSelectButtonClick);
    }

    public void SetLevel(Level level)
    {
        _level = level;
        RenderLevelItem(level);      
    }

    private void RenderLevelItem(Level level)
    {
        _number.text = (level.Number + 1).ToString();
        RenderSelect(level.isSelected);
        RenderStars(level.NumberOfStars);
    }

    private void RenderSelect(bool isSelect)
    {
        if (isSelect)
        {   
            _select.color = Color.yellow;
        }
        else
        {
            _select.color = Color.white;
        }
    }

    private void RenderStars(int numberOfStars)
    {
        for (int i = 0; i < _stars.Length; i++)
        {
            if (i < numberOfStars)
            {
                _stars[i].color = Color.yellow;
            }
            else
            {
                _stars[i].color = Color.white;
            }
        }
    }

    public void ChagneStateLevelSelected()
    {
        _level.Select(!_level.isSelected);
        RenderSelect(_level.isSelected);
        ChangeLevel?.Invoke(_level);
    }

    public void ChagneNumberOfStars(int numberOfStars)
    {
        _level.SetNumberOfStars(numberOfStars);
        RenderStars(numberOfStars);
        ChangeLevel?.Invoke(_level);
    }

    private void OnSelectButtonClick()
    {
        SelectButtonClick?.Invoke(_level, this);
    }


}
