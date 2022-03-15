using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PaintingАrea : MonoBehaviour
{
    public event UnityAction DropOutside;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.TryGetComponent<PaintDrop>(out PaintDrop drop) )
        {
            if (drop.gameObject.activeSelf == true)
            {
                DropOutside?.Invoke();
            }
        }
       
    }
}