﻿using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PlayerVFXLeftTrigger : MonoBehaviour
{
    private PlayerVFX playerVFX;

    private void Start()
    {
        playerVFX = transform.parent.GetComponent<PlayerVFX>();
        GetComponent<BoxCollider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag != "Pickup")
            playerVFX.IsLeftSideCol = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "Pickup")
            playerVFX.IsLeftSideCol = false;
    }

}