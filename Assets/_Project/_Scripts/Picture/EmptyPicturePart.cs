using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PictureAnimation))]
public class EmptyPicturePart : MonoBehaviour, IPicturePartData, IPicturePart
{
    [SerializeField] private int _number;
    [SerializeField] private Transform _paintbrushPosition;
    [SerializeField] private Color _color;
    [SerializeField] private Vector3 _dropSize = new Vector3(1, 1, 1);

    private Painting¿rea _painting¿rea;
    private ScaleMark[] _scaleMarks;
    private StateScaleMarks _stateMarks;

    public int Number { get => _number; }
    public Vector3 PaintbrushPosition { get => _paintbrushPosition.position; }
    public Color Color { get => _color; }
    public Vector3 DropSize { get => _dropSize; }

    public event UnityAction<StateScaleMarks> —hangedStateMarks;
    public event UnityAction DropOutsidePart;

    private void Awake()
    {
        _painting¿rea = GetComponentInChildren<Painting¿rea>();
        _scaleMarks = GetComponentsInChildren<ScaleMark>();
        //PaintbrushPosition = _paintbrushPosition.position;
    }

    private void OnEnable()
    {
        _painting¿rea.DropOutside += OnDropOutsidePart;
    }

    private void OnDisable()
    {
        _painting¿rea.DropOutside -= OnDropOutsidePart;
    }

    public bool CheckPartIsFilled()
    {
        int countMarkActivity = 0;
        foreach (var mark in _scaleMarks)
        {
            if (mark.CheckEnterPaint() == true)
            {
                countMarkActivity++;
            }
        }

        return countMarkActivity == _scaleMarks.Length;
    }

    private void OnDropOutsidePart()
    {
        DropOutsidePart?.Invoke();
    }

    private void Update()
    {
        if (CheckPartIsFilled() == true)
        {
            if (_stateMarks == StateScaleMarks.Passive)
            {
                _stateMarks = StateScaleMarks.Activated;
                —hangedStateMarks?.Invoke(_stateMarks);
            }
        }
        else
        {
            if (_stateMarks == StateScaleMarks.Activated)
            {
                _stateMarks = StateScaleMarks.Passive;
                —hangedStateMarks?.Invoke(_stateMarks);
            }
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
