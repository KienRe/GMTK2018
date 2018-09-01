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
            player = GetComponent<PlayerController>();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.IsOnSpeedbooster = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.IsOnSpeedbooster = false;
            player = null;
        }
    }
}