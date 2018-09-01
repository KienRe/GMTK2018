using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tacho : MonoBehaviour
{

    public float speed;
    [Tooltip("Maximale Anzahl in Grad")]
    public Vector2 tachoGrenze = new Vector2(0, 180);
    public Vector2 speedLimit = new Vector2(0, 100);
    public Transform tachoNadel;

    void Update()
    {
        float mappedValue = Remapper.Remap(speed, speedLimit.x, speedLimit.y, tachoGrenze.x, tachoGrenze.y);
        tachoNadel.localRotation = Quaternion.Euler(0f, 0f, -mappedValue);
    }
}
