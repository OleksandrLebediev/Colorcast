using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintbrushPlace : MonoBehaviour
{
    [SerializeField] private Paintbrush _paintbrush;
    [SerializeField] private HolderZone _holderZone;
    [SerializeField] private PaintContainer _paintContainer;
    [SerializeField] private PicturePlace _picturePlace;
    [SerializeField] private AudioPlayer _audioPlayer;

    private Vector3 _paintbrushLastPosition;
    private Color _paintbrushLastColor;
    private Vector3 _paintbrushLastDropSize;

    private void OnEnable()
    {
        _picturePlace.PictureComplete += RemovePaintbrush;
    }

    private void OnDisable()
    {
        _picturePlace.PictureComplete -= RemovePaintbrush;
    }

    public void SetPaintbrush(Paintbrush paintbrush)
    {
        if (_paintbrush != null)
        {
            RemovePaintbrush(_paintbrush);
        }
        _paintbrush = Instantiate(paintbrush, transform);
        UpdateStatePaintbrush(_paintbrush);
        SubscribePaintbrush(_paintbrush);
    }

    public void RemovePaintbrush(Paintbrush paintbrush)
    {
        UnsubscribePaintbrush(paintbrush);
        Destroy(paintbrush.gameObject);
    }

    public void RemovePaintbrush(IPictureData pictureData)
    {
        RemovePaintbrush(_paintbrush);
    }

    private void UpdateStatePaintbrush(EmptyPicturePart picturePart)
    {
        _paintbrush.Move(picturePart.PaintbrushPosition);
        _paintbrush.SetDropParameters(picturePart.Color, picturePart.DropSize);


        _paintbrushLastColor = picturePart.Color;
        _paintbrushLastPosition = picturePart.PaintbrushPosition;
        _paintbrushLastDropSize = picturePart.DropSize;
    }

    private void UpdateStatePaintbrush(Paintbrush paintbrush)
    {
        paintbrush.SetPosition(_paintbrushLastPosition);
        paintbrush.SetDropParameters(_paintbrushLastColor, _paintbrushLastDropSize);
    }

    private void SubscribePaintbrush(Paintbrush paintbrush)
    {
        _holderZone.Hold += paintbrush.OnStartPaintingProcess;
        _holderZone.StopHold += paintbrush.OnStopPaintingProcess;
        _picturePlace.ShowedEmptyPicturePart += UpdateStatePaintbrush;
        paintbrush.Painting += _paintContainer.SetPaintDrop;
        paintbrush.StartPaintingProcess += _audioPlayer.StartPlayPaintingProcessClip;
        paintbrush.StopPaintingProcess += _audioPlayer.StopPlayPaintingProcessClip;
    }

    private void UnsubscribePaintbrush(Paintbrush paintbrush)
    {
        _holderZone.Hold -= paintbrush.OnStartPaintingProcess;
        _holderZone.StopHold -= paintbrush.OnStopPaintingProcess;
        _picturePlace.ShowedEmptyPicturePart -= UpdateStatePaintbrush;
        paintbrush.Painting -= _paintContainer.SetPaintDrop;
        paintbrush.StartPaintingProcess -= _audioPlayer.StartPlayPaintingProcessClip;
        paintbrush.StopPaintingProcess -= _audioPlayer.StopPlayPaintingProcessClip;
    }
}
