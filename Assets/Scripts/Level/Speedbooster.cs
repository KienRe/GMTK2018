using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Speedbooster : MonoBehaviour
{
    private PlayerController player;

    private void Start()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player = other.GetComponent<PlayerController>();
            player.IsOnSpeedbooster = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (player != null && other.gameObject.tag == "Player")
        {
            player.IsOnSpeedbooster = false;
            player = null;
        }
    }
}