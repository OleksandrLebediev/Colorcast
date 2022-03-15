
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleMark : MonoBehaviour
{
    private BoxCollider2D _boxCollider;
    private SpriteRenderer _spriteRenderer;

    private readonly Color _colorRest = Color.white;
    private readonly Color _colorActivity = Color.green;

    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public bool CheckEnterPaint()
    {
        Collider2D collider = Physics2D.OverlapBox(_boxCollider.bounds.center, _boxCollider.bounds.size, 0, PaintContainer.LayerMaskDrop);
        bool enterPaintDrop = collider != null;
        SetScaleMarkCollor(enterPaintDrop ? _colorActivity : _colorRest);
        return enterPaintDrop;
    }

    public void SetScaleMarkCollor(Color color)
    {
        _spriteRenderer.color = color;
    }
}
public enum StateScaleMarks
{
    Passive,
    Activated
}
