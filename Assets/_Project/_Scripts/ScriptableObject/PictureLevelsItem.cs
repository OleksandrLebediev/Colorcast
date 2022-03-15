using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Level", menuName = "Picture Levels", order = 51)]
public class PictureLevelsItem : ScriptableObject
{
    [SerializeField] private Picture _picture;
    [SerializeField] private int _number;

    public bool isCompleted { get; private set; }

    public Picture Picture => _picture;
    public int Number => _number;

    public void Competed()
    {
        isCompleted = true;
    }
}
