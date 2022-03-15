using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelsDisplay : MonoBehaviour
{
    public readonly Color NormalStateColor = new Color(0, 0, 0, 0);
    public readonly Color SelectedStateColor = new Color(1, 1, 1, 0.3f);
    public readonly Color CompletedStateColor = new Color(0, 0.67f, 1, 1);

    [SerializeField] private PicturePlace _picturePlace;
    [SerializeField] private PictureDispenser _pictureDispenser;
    [SerializeField] private DisplayCircleLevel _displayCircleLevelTemplate;
    [SerializeField] private GameObject _compound;
    [SerializeField] private Transform _container;
    [SerializeField] private TMP_Text _numberLevel;

    private List<DisplayCircleLevel> _displayCircleLevels = new List<DisplayCircleLevel>();
   
    private void OnEnable()
    {
        _pictureDispenser.PictureChange += OnPictureChange;
        _picturePlace.ShowedEmptyPicturePart += OnChangePicturePart;
        _picturePlace.PictureComplete += OnPictureComplete;
    }

    private void OnDisable()
    {
        _pictureDispenser.PictureChange -= OnPictureChange;
        _picturePlace.ShowedEmptyPicturePart -= OnChangePicturePart;
        _picturePlace.PictureComplete -= OnPictureComplete;
    }

    private void OnPictureChange(Picture picture)
    {
        _numberLevel.text = $"Level {picture.ID + 1}";
        RenderDisplayCircleLevel(picture.Count);
    }

    private void OnChangePicturePart(EmptyPicturePart empty)
    {
        ChangeCurrentPart(empty.Number);
        ChangeCompletedPart(empty.Number - 1);
    }

    private void RenderDisplayCircleLevel(int count)
    {
        GameObject compound = null;
        RemoveCircleContainer();

        for (int i = 0; i < count; i++)
        {
            DisplayCircleLevel circleLevel = Instantiate(_displayCircleLevelTemplate, _container);
            compound = Instantiate(_compound, _container);
            circleLevel.SetNumber(i + 1);
            circleLevel.RenderState(NormalStateColor);
            _displayCircleLevels.Add(circleLevel);
        }
        Destroy(compound);
    }

    private void ChangeCompletedPart(int number)
    {
        if (number < 0) return;
        _displayCircleLevels[number].RenderState(CompletedStateColor);
    }

    private void ChangeCurrentPart(int number)
    {
        _displayCircleLevels[number].RenderState(SelectedStateColor);
    }

    private void RemoveCircleContainer()
    {
        _displayCircleLevels.Clear();

        for (int i = 0; i < _container.childCount; i++)
        {
            Destroy(_container.GetChild(i).gameObject);
        }
    }

    private void OnPictureComplete(Picture picture)
    {
        for (int i = 0; i < _displayCircleLevels.Count; i++)
        {
            ChangeCompletedPart(i);
        }
    }

}
