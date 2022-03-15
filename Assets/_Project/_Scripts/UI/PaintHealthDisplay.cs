using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PaintHealthDisplayAnimations))]
public class PaintHealthDisplay : MonoBehaviour
{
    private HealthPoint[] _healthPoints;
    private int _currentNumberBlot;
    private PaintHealthDisplayAnimations _animations;
    
    private void Awake()
    {
        _healthPoints = GetComponentsInChildren<HealthPoint>();
        _animations = GetComponent<PaintHealthDisplayAnimations>();
        Debug.Log(Application.persistentDataPath);
    }

    public void Show()
    {
        foreach (var health in _healthPoints)
        {
            health.RebindAnimations();
        }

        gameObject.SetActive(true);
        _animations.RebindAnimations();
        _animations.ShowAnimation();
        _currentNumberBlot = 0;
    }

    public void Hide()
    {        
        _animations.HideAnimation();
        gameObject.SetActive(false);
    }

    public void UpdateHealth(int health)
    {
        _healthPoints[_currentNumberBlot].LossEffect();
        _currentNumberBlot++;
    }

    public void SetHealthPointsColor(Color color)
    {
        foreach (var health in _healthPoints)
        {
            health.SetColor(color);
        }
    }
}
