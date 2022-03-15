using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PicturePlace : MonoBehaviour
{
    private readonly Vector3 _startPosition = new Vector3(0, -4, 0);

    [SerializeField] private TimerForCheck _timerForCheck;
    [SerializeField] private PaintHealthDisplay _paintHealthDisplay;

    private Picture _picture;
    public Picture Picture => _picture;

    public event UnityAction<EmptyPicturePart> ShowedEmptyPicturePart;
    public event UnityAction<Picture> PictureComplete;
    public event UnityAction<Picture> PictureStart;
    public event UnityAction<Picture> PictureFail;

    public void SetPicture(Picture picture)
    {
        if (_picture != null)
        {
            RemovePicture(_picture);
        }
        _picture = Instantiate(picture, _startPosition, Quaternion.identity, transform);
        transform.localScale = Vector3.one;

        SubscribePicture(_picture);
        OnPictureStart(_picture);
    }


    public void RemovePicture(Picture picture)
    {
        UnsubscribePicture(picture);
        Destroy(picture.gameObject);
    }


    private void OnShowedEmptyPicturePart(EmptyPicturePart emptyPart)
    {
        ShowedEmptyPicturePart?.Invoke(emptyPart);
    }

    private void OnPictureComplete(Picture picture)
    {
        PictureComplete?.Invoke(picture);
    }

    private void OnPictureStart(Picture picture)
    {
        PictureStart?.Invoke(picture);
    }
    
    private void OnPictureFail(Picture picture)
    {
        PictureFail?.Invoke(picture);
    }

    private void SubscribePicture(Picture picture)
    {
        picture.ÑhangedStateMarksInThePart += _timerForCheck.SetTimerState;
        picture.ShowedPicturePart += OnShowedEmptyPicturePart;
        picture.Complete += OnPictureComplete;
        picture.Fail += OnPictureFail;
        picture.HealthChange += _paintHealthDisplay.UpdateHealth;
        _timerForCheck.PicturePartCompleted += picture.TryOpenFullPart;
    }

    private void UnsubscribePicture(Picture picture)
    {
        picture.ÑhangedStateMarksInThePart -= _timerForCheck.SetTimerState;
        picture.ShowedPicturePart -= OnShowedEmptyPicturePart;
        picture.Complete -= OnPictureComplete;
        picture.Fail -= OnPictureFail;
        picture.HealthChange -= _paintHealthDisplay.UpdateHealth;
        _timerForCheck.PicturePartCompleted -= picture.TryOpenFullPart;
    }
}
