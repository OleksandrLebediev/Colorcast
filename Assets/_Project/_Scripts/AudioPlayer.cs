using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip _paintingProcessClip;
    [SerializeField] private AudioClip _pictureCompletedClip;
    [SerializeField] private AudioClip _timerClip;

    [SerializeField] private PicturePlace _picturePlace;
    [SerializeField] private HolderZone _holderZone;
    [SerializeField] private TimerForCheck _timer;

    private AudioSource _audioSource;
    private Coroutine _waitSoundPaitingProcessCorutine;
    private Coroutine _waitSoundTimerCorutine;
    private float _delayPaintingSound = 0.2f;
    private float _delayTimerSound = 0.35f;
    private float _delayBeforeTimerSound = 0.2f;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _timer.Activated += StartPlayTimerClip;
        _timer.Deactivating += StopPlayTimerClip;
        _picturePlace.PictureComplete += PlayPicturecompletedClip;
    }

    private void OnDisable()
    {
        _timer.Activated -= StartPlayTimerClip;
        _timer.Deactivating -= StopPlayTimerClip;
        _picturePlace.PictureComplete -= PlayPicturecompletedClip;
    }

    public void StartPlayPaintingProcessClip()
    {
        _waitSoundPaitingProcessCorutine = StartCoroutine(WaitSoundPaitingProcessCorutine());
    }

    public void StopPlayPaintingProcessClip()
    {
        if(_waitSoundPaitingProcessCorutine != null)
        {
            StopCoroutine(_waitSoundPaitingProcessCorutine);
        }
        _audioSource.Stop();
    }

    private void StartPlayTimerClip()
    {
        _waitSoundTimerCorutine = StartCoroutine(WaitSoundTimerCorutine());
    }
    
    private void StopPlayTimerClip()
    {
        if (_waitSoundTimerCorutine != null)
        {
            StopCoroutine(_waitSoundTimerCorutine);
        }
    }

    private void PlayPicturecompletedClip(IPictureData pictureData)
    {
        _audioSource.PlayOneShot(_pictureCompletedClip);
    }

    private IEnumerator WaitSoundPaitingProcessCorutine()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_delayPaintingSound);

        while (true)
        {
            _audioSource.PlayOneShot(_paintingProcessClip);
            yield return waitForSeconds;
        }
    }
    
    private IEnumerator WaitSoundTimerCorutine()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_delayTimerSound);
        yield return new WaitForSeconds(_delayBeforeTimerSound);

        while (true)
        {
            _audioSource.PlayOneShot(_timerClip);
            yield return waitForSeconds;
        }
    }
}
