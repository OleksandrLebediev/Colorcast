using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintbrushMovement : MonoBehaviour
{
    private readonly float _speed = 10;
    private Coroutine _currentMovement;

    public void MovementForPicturePart(Vector3 target)
    {
        if (_currentMovement == null)
        {
            _currentMovement = StartCoroutine(Movement(target));
        }
    }

    private IEnumerator Movement(Vector3 target)
    {     
        while (true)
        {
            transform.position = Vector3.Lerp(transform.position, target, _speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, target) < 0.001f)
            {    
                _currentMovement = null;
                yield break;
            }
            yield return null;
        }
    }
}
