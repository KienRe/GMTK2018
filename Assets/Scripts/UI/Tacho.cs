using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tacho : MonoBehaviour
{

    [Tooltip("Maximale Anzahl in Grad")]
    public Vector2 tachoGrenze = new Vector2(0, 180);
    public Vector2 speedLimit = new Vector2(0, 100);
    public Transform tachoNadel;
    public Text speedText;
    private PlayerController player;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        speedLimit = new Vector2(player.minSpeed, player.maxSpeed);
    }

    void Update()
    {
        float mappedValue = Remapper.Remap(player.currentSpeed, speedLimit.x, speedLimit.y, tachoGrenze.x, tachoGrenze.y);
        tachoNadel.localRotation = Quaternion.Euler(0f, 0f, -mappedValue);
        speedText.text = player.currentSpeed.ToString("0");
    }
}
