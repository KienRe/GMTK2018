using System;
using System.Collections;
using UnityEngine;

public class MetalBar : MonoBehaviour
{
    [Header("Settings")]
    public float floatHeight = 2f;
    public float rotSpeed = 20f;
    //PRIVATE
    private Vector3 startPos;

    //EVENTS
    public static event Action OnMetalBarPickup = delegate { };

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        Vector3 floatVec = transform.up * (Mathf.Sin(Time.time) + 1) * floatHeight;
        transform.position = new Vector3(startPos.x + floatVec.x, startPos.y + floatVec.y, startPos.z + floatVec.z);
        transform.rotation *= Quaternion.Euler(0, rotSpeed * Time.deltaTime, 0);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            OnMetalBarPickup();
            Debug.Log("Metal Bar has been picked up!");
            gameObject.SetActive(false);
        }
    }
}
