using System.Collections;
using UnityEngine;

public interface IPaintbrushPaintingData 
{
    public Vector3 PointSpawnPaintDrop { get; }
    public Vector3 DropSize { get; }
    public Color DropColor { get; }
}
