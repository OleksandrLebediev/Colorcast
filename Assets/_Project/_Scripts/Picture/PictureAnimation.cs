using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PictureAnimation : MonoBehaviour
{
    private readonly float _speed = 5f;
    private readonly string _nameAnimationShowPicture = "Appearance";
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponentInParent<Animator>();
        _animator.enabled = false;
    }

    public void ShowFullPicture(UnityAction animation—ompleted)
    {
        _animator.enabled = true;
        _animator.SetTrigger(_nameAnimationShowPicture);
        AnimationClip clip = _animator.runtimeAnimatorController.animationClips[0];
        float timeAnimation = clip.length;
        StartCoroutine(Wait(timeAnimation, animation—ompleted));
    }

    public void ShowPart(Transform picturePart)
    {
        picturePart.localScale = Vector3.zero;
        LeanTween.scale(picturePart.gameObject, Vector3.one, 0.3f).setEase(LeanTweenType.animationCurve);
    }

    private IEnumerator Wait(float time, UnityAction animation—ompleted)
    {
        yield return new WaitForSeconds(time);
        animation—ompleted?.Invoke();
    }
}
