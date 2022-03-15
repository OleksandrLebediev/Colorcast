using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PaintbrushDispenser : MonoBehaviour
{
    private const string CURRENT_PAINTBRUSH = "CurrentPaintbrush";

    [SerializeField] private PaintbrushPlace _paintbrushPlace;
    [SerializeField] private PictureDispenser _pictureDispenser;
    [SerializeField] private List<Paintbrush> _paintbrushTemplates;
    

    private int _numberPaintbrush = 0;
    private PlayerPrefsSystem _playerPrefsSystem = new PlayerPrefsSystem(CURRENT_PAINTBRUSH);

    public IEnumerable<Paintbrush> Paintbrushes => _paintbrushTemplates;
    public Paintbrush ÑurrentPaintbrush => _paintbrushTemplates[_numberPaintbrush];

    private void OnEnable()
    {
        _pictureDispenser.PictureChange += (Paintbrush) => { DispencePaintbrush(); };
    }

    private void OnDisable()
    {
        _pictureDispenser.PictureChange -= (Paintbrush) => { DispencePaintbrush(); };
    }

    private void Awake()
    {
        _paintbrushTemplates = _paintbrushTemplates.OrderBy(paintbrush => paintbrush.ID).ToList();
    }

    public void DispencePaintbrush()
    {
        _numberPaintbrush = _playerPrefsSystem.Load();
        DispencePaintbrush(ÑurrentPaintbrush);
    }

    public void DispencePaintbrush(Paintbrush paintbrush)
    {
        _paintbrushPlace.SetPaintbrush(paintbrush);
        _numberPaintbrush = paintbrush.ID;
        _playerPrefsSystem.Save(paintbrush.ID);
    }
}
