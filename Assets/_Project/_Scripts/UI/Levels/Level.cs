using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Level 
{
    public Level (int number, int numberOfStars, bool isSelected)
    {
        _number = number;
        _numberOfStars = numberOfStars;
        _isSelected = isSelected;
    }

    private int _number;
    private int _numberOfStars;
    private bool _isSelected;

    public int Number => _number;
    public int NumberOfStars => _numberOfStars;
    public bool isSelected => _isSelected;

    public void Select(bool value)
    {
        _isSelected = value;
    }    

    public void SetNumberOfStars(int numberOfStars)
    {
        _numberOfStars = numberOfStars;
    }
}
