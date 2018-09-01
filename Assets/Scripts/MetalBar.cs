using System;
using UnityEngine;

public class MetalBar : MonoBehaviour
{
    public static event Action OnMetalBarPickup = delegate { };

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            OnMetalBarPickup();
            Debug.Log("Metal Bar has been picked up!");
        }
    }
}
