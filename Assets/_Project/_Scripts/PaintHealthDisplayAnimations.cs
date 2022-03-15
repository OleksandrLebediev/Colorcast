
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PaintHealthDisplayAnimations : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void ShowAnimation()
    {
        _animator.SetTrigger("Show");
    }

    public void HideAnimation()
    {
        _animator.SetTrigger("Hide");
    }

    public void RebindAnimations()
    {
        _animator.Rebind();
    }
}
