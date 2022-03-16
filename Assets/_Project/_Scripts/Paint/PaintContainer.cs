using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Pool;

public class PaintContainer : MonoBehaviour
{
    public const int LayerMaskDrop = 1 << 6;

    [SerializeField] private PaintDrop _paintDropTemplate;
    [SerializeField] private Material _paintMaterial;
    [SerializeField] private TimerForCheck _timerForCheck;
    [SerializeField] private PicturePlace _picturePlace;

    private readonly int _maxPoolSize = 300;

    private List<PaintDrop> _paintDrops = new List<PaintDrop>();
    private ObjectPool<PaintDrop> _pool;

    private void OnEnable()
    {
        _timerForCheck.PicturePartCompleted += RemovePaintDrop;
        _picturePlace.PictureStart += (Picture picture ) => { RemovePaintDrop(); };
    }

    private void OnDisable()
    {
        _timerForCheck.PicturePartCompleted -= RemovePaintDrop;
        _picturePlace.PictureStart -= (Picture picture) => { RemovePaintDrop(); };
    }

    private void Start()
    {
        _pool = new ObjectPool<PaintDrop>(
            () => { return Instantiate(_paintDropTemplate); },
            drop => { drop.gameObject.SetActive(true); },
            drop => { drop.gameObject.SetActive(false); },
            drop => { Destroy(drop.gameObject); },
            false, 10, _maxPoolSize);
    }

    public void SetPaintDrop(IPaintbrushPaintingData paintbrushPaintingData)
    {
        PaintDrop paintDrop = _pool.Get();
        paintDrop.transform.position = paintbrushPaintingData.PointSpawnPaintDrop;
        paintDrop.transform.localScale = paintbrushPaintingData.DropSize;
        paintDrop.transform.SetParent(transform);
        _paintDrops.Add(paintDrop);
        _paintMaterial.color = paintbrushPaintingData.DropColor;
    }

    public void RemovePaintDrop()
    {
        foreach (var paintDrop in _paintDrops)
        {
            _pool.Release(paintDrop);
        }
        _paintDrops.Clear();
    }
}
