using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class HealthPoint : MonoBehaviour
{
    [SerializeField] private Image _body;
    [SerializeField] private Image _cross;
    [SerializeField] private Animator _animator;

    private readonly string LossAnimation = "Loss";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetColor(Color color)
    {
        _body.color = color;
    }

    public void LossEffect()
    {
        _animator.SetTrigger(LossAnimation);
    }

    public void RebindAnimations()
    {
        _animator.Rebind();
        _cross.gameObject.SetActive(false);
    }
}
