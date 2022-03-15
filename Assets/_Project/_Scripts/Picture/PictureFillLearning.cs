using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PictureFillLearning : MonoBehaviour
{
    [SerializeField] private GameObject _LeftFillMark;
    [SerializeField] private GameObject _RightFillMark;
    [SerializeField] private TMP_Text _learningText;

    private float _speed = 1f;
    private float _leftPointMoveTarget = -2f;
    private float _rigtPointoveTarget = 2f;

    private void Start()
    {
        Train();
    }

    private void Train()
    {
        LeanTween.moveLocalX(_LeftFillMark, _leftPointMoveTarget, _speed).setEaseInOutQuad().setLoopPingPong();
        LeanTween.moveLocalX(_RightFillMark, _rigtPointoveTarget, _speed).setEaseInOutQuad().setLoopPingPong();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
