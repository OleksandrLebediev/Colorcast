using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class PictureDispenser : MonoBehaviour
{
    [SerializeField] private LevelBuilder _levelBuilder;
    [SerializeField] private PicturePlace _picturePlace;
    [SerializeField] private UIScreensHandler _uiScreenHandler;
    [SerializeField] private List<Picture> _pictureTemplates;

    private int _numberPicture; 

    private Picture _currentPicture;
    public IEnumerable<Picture> Pictures => _pictureTemplates;

    public event UnityAction<Picture> PictureChange;


    private void OnEnable()
    {
        _uiScreenHandler.VictoryScreenClosed += DispenceNextPicture;
        _uiScreenHandler.LossScreenClosed += DispencePicture;
    }

    private void OnDisable()
    {
        _uiScreenHandler.VictoryScreenClosed -= DispenceNextPicture;
        _uiScreenHandler.LossScreenClosed -= DispencePicture;
    }

    private void Awake()
    {
#if UNITY_EDITOR
        int level = 0;
        foreach (var picture in _pictureTemplates)
        {
            if (level != picture.ID) Debug.LogError("Level dont id Picture", this);
            level++;
        }
#endif
    }

    private void Start()
    {
        DispencePicture();
    }

    public void DispencePicture()
    {
        _numberPicture = _levelBuilder.GetCurrentLevel();
        DispencePicture(_numberPicture);
    }

    public void DispencePicture(int numberPicture)
    {
        _currentPicture = _pictureTemplates[numberPicture];
        DispencePicture(_currentPicture);

    }

    public void DispencePicture(Picture picture)
    {
        _picturePlace.SetPicture(picture);
        _levelBuilder.SetCurrentLevel(picture.ID);
        PictureChange?.Invoke(_picturePlace.Picture);
    }

    public void DispenceNextPicture()
    {
        _numberPicture = GetNextPictureIndex();
        DispencePicture(_numberPicture);
    }

    private int GetNextPictureIndex()
    {
        int nextIndex = _numberPicture + 1;

        if (nextIndex >= _pictureTemplates.Count)
        {
            nextIndex = 0;
        }

        return nextIndex;
    }
}
