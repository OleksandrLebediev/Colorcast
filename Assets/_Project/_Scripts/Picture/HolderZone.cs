using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class HolderZone : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public event UnityAction Hold;
    public event UnityAction StopHold;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        Hold?.Invoke();

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        StopHold?.Invoke();
    }

    public void Activated()
    {
        gameObject.SetActive(true);
    }

    public void Deactivated()
    {
        gameObject.SetActive(false);
    }
}

