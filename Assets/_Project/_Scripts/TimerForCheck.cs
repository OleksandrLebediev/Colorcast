using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimerForCheck : MonoBehaviour
{
    [SerializeField] private TimerDisplay _timerDisplay;
    [SerializeField] private LossScreen  _losingDisplay;
    [SerializeField] private LevelBuilder _levelBuilder;

    private Coroutine _timer;
    private float _currentTime;

    private readonly float _timeWait = 2f; 
    private readonly float _timeDelay = 0.3f;

    public event UnityAction PicturePartCompleted;
    public event UnityAction Activated;
    public event UnityAction Deactivating;

    private void Start()
    {
        Deactivated();
    }

    public void SetTimerState(StateScaleMarks state)
    {
        if(state == StateScaleMarks.Activated)
        {
            Activate();
        }
        else if(state == StateScaleMarks.Passive)
        {
            Deactivated();
        }
    }

    private void Activate()
    {
        _timer = StartCoroutine(Timer());
        Activated?.Invoke();
    }

    private void Deactivated()
    {
        if (_timer != null)
        {
            StopCoroutine(_timer);
        }
        _timerDisplay.Hide();
        Deactivating?.Invoke();
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(_timeDelay);
        _timerDisplay.Show();
        _currentTime = 0;

        while (true)
        {
            _currentTime += Time.deltaTime;
            float ratioTime = _currentTime / _timeWait;

            _timerDisplay.UpdateLineOfTimer(1 - ratioTime);

            if (_currentTime >= _timeWait)
            {
                PicturePartCompleted?.Invoke();
                Deactivated();
                yield break;
            }

            yield return null;
        }
    }
}
