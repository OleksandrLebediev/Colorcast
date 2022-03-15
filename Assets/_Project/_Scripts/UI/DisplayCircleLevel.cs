using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DisplayCircleLevel : MonoBehaviour
{
    [SerializeField] private TMP_Text _number;
    [SerializeField] private Image _selectedCircle;

    public void SetNumber(int number)
    {
        _number.text = number.ToString();
    }

    public void RenderState(Color color)
    {
        _selectedCircle.color = color;
    }
}
