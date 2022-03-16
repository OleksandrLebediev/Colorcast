using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

[RequireComponent(typeof(PaintbrushMovement))]
[RequireComponent(typeof(PaintbrushAnimation))]
public class Paintbrush : MonoBehaviour, IPaintbrushPaintingData
{
    [SerializeField] private int _id;
    [SerializeField] private Transform _pointSpawnPaintDrop;

    private readonly Vector3 _normalSize = Vector3.one;
    private readonly Vector3 _bigSize = new Vector3(1.2f, 1.2f, 1);
    private readonly float _speedAnimation = 10f;

    private float _delayPainting = 0.01f;
    private Coroutine _waitColoration;
    private PaintbrushMovement _paintbrushMovement;
    private PaintbrushAnimation _paintbrushAnimation;
    private Color _dropColor;
    private Vector3 _dropSize;
    private Sprite _icon;
    
    public int ID => _id;
    public Sprite Icon
    {
        get 
        { 
            if (_icon == null)
            {
                _icon = GetComponentInChildren<SpriteRenderer>().sprite; 
            }
            return _icon;
        }
    }

    public Vector3 PointSpawnPaintDrop => _pointSpawnPaintDrop.position;
    public Vector3 DropSize => _dropSize;
    public Color DropColor => _dropColor;

    public event UnityAction<IPaintbrushPaintingData> Painting;
    public event UnityAction PaintingSound;
    public event UnityAction StartPaintingProcess;
    public event UnityAction StopPaintingProcess;

    private void Awake()
    {
        _paintbrushMovement = GetComponent<PaintbrushMovement>();
        _paintbrushAnimation = GetComponent<PaintbrushAnimation>();
    }

    private void Start()
    {
        StopPaintingProcess?.Invoke();
    }

    public void SetDropParameters(Color color, Vector3 dropSize)
    {
        _dropColor = color;
        _dropSize = dropSize;
    }

    public void OnStartPaintingProcess()
    {
        _waitColoration = StartCoroutine(IntensityOfPaintingProcessCorutine());
        StartPaintingProcess?.Invoke();
    }

    public void OnStopPaintingProcess()
    {
        if (_waitColoration != null) {
            StopCoroutine(_waitColoration);
        }
        _paintbrushAnimation.ChangeSize(transform, _normalSize, _speedAnimation);
        StopPaintingProcess?.Invoke();
    }

    public void Move(Vector3 target)
    {
        _paintbrushMovement.MovementForPicturePart(target);
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    private IEnumerator IntensityOfPaintingProcessCorutine()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_delayPainting);

        while (true)
        {
            _paintbrushAnimation.ChangeSize(transform, _bigSize, _speedAnimation);
            Painting?.Invoke(this);
            yield return waitForSeconds;
        }
    } 
    

}




