using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : MonoBehaviour
{
    private PlayerController player;
    public float safeSpeed = 10f;

    private void Update()
    {
        if (player != null && player.currentSpeed <= safeSpeed)
        {
            StartCoroutine(player.SlowCart());
            player = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.GetComponentInChildren<PlayerController>();
        }
    }
}

