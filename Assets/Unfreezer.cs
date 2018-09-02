using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unfreezer : MonoBehaviour {

    public bool unfreeze = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (unfreeze)
            {
                other.attachedRigidbody.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            }
            else
            {
                other.attachedRigidbody.constraints = RigidbodyConstraints.FreezeRotationX |RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            }
        }
    }
}
