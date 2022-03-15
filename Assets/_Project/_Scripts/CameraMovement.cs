using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private FirstClickHandler _clickZone;
    [SerializeField] private UIScreensHandler _uIScreensHandler;

    private readonly Vector3 _startPosition  = new Vector3 (0, -1, -10);
    private readonly Vector3 _target = new Vector3(0, 0, -10);
    private readonly float _speed = 5;
    private readonly float _minDistanc = 0.001f;

    private void OnEnable()
    {
        _clickZone.FirstClick += Move;
        _uIScreensHandler.UpdatingUIForNewStart += SetStartPosition;
    }

    private void OnDisable()
    {
        _clickZone.FirstClick -= Move;
        _uIScreensHandler.UpdatingUIForNewStart -= SetStartPosition;
    }


    private void Start()
    {
        SetStartPosition();
    }

    private void SetStartPosition()
    {
        transform.position = _startPosition;
    }

    private void Move() 
    {
      StartCoroutine(PlannedMovement(_target));
    }

    private IEnumerator PlannedMovement(Vector3 traget)
    { 
        while (true)
        {
            transform.position = Vector3.Lerp(transform.position, traget, Time.deltaTime * _speed);
            if (Vector3.Distance(transform.position, traget) <= _minDistanc)
            {
                yield break;
            }
            yield return null;
        }
    }
}
