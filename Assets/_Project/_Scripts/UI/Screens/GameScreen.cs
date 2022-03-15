using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScreen : MonoBehaviour
{
    [SerializeField] PaintHealthDisplay _healthDisplay;

    public void Show()
    {
        gameObject.SetActive(true);
        _healthDisplay.Show();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        _healthDisplay.Hide();
    }

    public void UpdateScreen(IPicturePartData picturePart)
    {
        _healthDisplay.SetHealthPointsColor(picturePart.Color);
    }
}
