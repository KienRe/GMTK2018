using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.transform.CompareTag("Pickup"))
        {
            Debug.LogFormat("Killed by {0}", other.transform.name);
            FindObjectOfType<MainManager>().SwitchGameplay(GameState.LOST);
        }
    }
}
