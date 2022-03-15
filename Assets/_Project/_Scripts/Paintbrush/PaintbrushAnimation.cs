using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintbrushAnimation : MonoBehaviour
{
    private readonly float _minDistanc = 0.001f;
    private Coroutine _plannedChangeSize;


    public void ChangeSize(Transform transform, Vector3 targetSize, float speed)
    {
        if(_plannedChangeSize != null)
        {
            StopCoroutine(_plannedChangeSize);
        }
        _plannedChangeSize = StartCoroutine(PlannedChangeSize(transform, targetSize, speed));
    }

    private IEnumerator PlannedChangeSize(Transform transform, Vector3 targetSize, float speed)
    {
        while (true)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, targetSize, Time.deltaTime * speed);
            if (Vector3.Distance(transform.localScale, targetSize) <= _minDistanc)
            {
                _plannedChangeSize = null;
                yield break;
            }
            yield return null;
        }
    }
}
