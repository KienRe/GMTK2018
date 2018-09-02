using UnityEngine;

public class RotationConstraint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.transform.rotation = Quaternion.Euler(45f, 0f, 0f);
            other.gameObject.GetComponent<PlayerController>().rigid.constraints = RigidbodyConstraints.FreezeRotation; 
        }
    }
}