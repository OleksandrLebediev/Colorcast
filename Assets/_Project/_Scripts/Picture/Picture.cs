using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PictureAnimation))]
public class Picture : MonoBehaviour, IPictureData
{
    [SerializeField] private FullPart _fullPart;
    [SerializeField] private int _id;

    public int ID { get => _id; }
    public int Count => _pictureParts.Count - 1;
    public bool Done => _emptyParts.Count == _numberNextPart;
    public bool IsFail => Health == 0;
    public int Health => _currentHealth;
    public EmptyPicturePart CurentEmptyPart { get; private set; }

    private List<EmptyPicturePart> _emptyParts;
    private List<IPicturePart> _pictureParts;
    private PictureAnimation _animation;
    private PictureFillLearning _pictureFillLearning;
    private int _numberNextPart = 0;
    private int _currentHealth;
    private Vector3 _centerPicture;

    private readonly int _health = 3;

    public event UnityAction<EmptyPicturePart> ShowedPicturePart;    
    public event UnityAction<Picture> Complete;
    public event UnityAction<StateScaleMarks> ÑhangedStateMarksInThePart;
    public event UnityAction<Picture> Fail;
    public event UnityAction<int> HealthChange;
    public event UnityAction PictureAnimationComplete;

    private void Awake()
    {
        _emptyParts = new List<EmptyPicturePart>(GetComponentsInChildren<EmptyPicturePart>());
        _animation = GetComponent<PictureAnimation>();
        _pictureFillLearning = GetComponentInChildren<PictureFillLearning>();
        _pictureParts = new List<IPicturePart>(_emptyParts);
        _pictureParts.Add(_fullPart);
    }

    private void Start()
    {
        _currentHealth = _health;
        _centerPicture = GetCenter();
        HideAllPart();
        TryOpenFullPart();
    }

    public void TryOpenFullPart()
    {
        RemovePart(CurentEmptyPart);

        if (Done == true)
        {
            transform.position = _centerPicture;
            transform.parent.localScale = Vector3.zero;
            _pictureFillLearning?.Hide();
            _animation.ShowFullPicture(PictureAnimationComplete);
            _fullPart.gameObject.SetActive(true);
            Complete?.Invoke(this);
        }
        else
        {
            NextEmptyPart();
        }
    }

    public void OnHealthChange()
    { 
        if (_currentHealth <= 0) return;

        _currentHealth--;
        if (IsFail == true)
        {
            Fail?.Invoke(this);
            ÑhangedStateMarksInThePart?.Invoke(StateScaleMarks.Passive);
        }
        HealthChange?.Invoke(_currentHealth);
    }

    private void NextEmptyPart()
    {
        CurentEmptyPart = _emptyParts[_numberNextPart];
        PreparePartForPainting(CurentEmptyPart);
        _numberNextPart++;
    }

    private void OnÑhangedStateMarksInThePart(StateScaleMarks state)
    {
        if (IsFail == true)
        {
            ÑhangedStateMarksInThePart?.Invoke(StateScaleMarks.Passive);
            return;
        }
        ÑhangedStateMarksInThePart?.Invoke(state);
    }

    private void PreparePartForPainting(EmptyPicturePart emptyPart)
    {
        emptyPart.ÑhangedStateMarks += OnÑhangedStateMarksInThePart;
        emptyPart.DropOutsidePart += OnHealthChange;
        ShowedPicturePart?.Invoke(emptyPart);
        ShowEmptyPart(emptyPart);
    }

    private void RemovePart(EmptyPicturePart emptyPart)
    {
        if (emptyPart is null) return;

        HideEmptyPart(emptyPart);
        emptyPart.ÑhangedStateMarks -= OnÑhangedStateMarksInThePart;
        emptyPart.DropOutsidePart -= OnHealthChange;
    }

    private void ShowEmptyPart(EmptyPicturePart emptyPart)
    {
        emptyPart.gameObject.SetActive(true);
        _animation.ShowPart(emptyPart.transform);
    }

    private void HideEmptyPart(EmptyPicturePart emptyPart)
    {
        emptyPart.gameObject.SetActive(false);
    }

    private void HideAllPart()
    {
        foreach (IPicturePart part in _pictureParts)
        {
            part.Hide();
        }
    }

    private Vector3 GetCenter()
    {
        SpriteRenderer sprite = _fullPart.GetComponent<SpriteRenderer>();
        Vector3 center = new Vector3(0, -sprite.bounds.extents.y, 0); 
        return center;
    }
}