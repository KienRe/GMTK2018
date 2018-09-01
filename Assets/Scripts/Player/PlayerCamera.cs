using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private float shakeDuration = 0f;
    private float shakeAmount = 0f;
    private float decreaseFactor = 0f;

    private Vector3 localStartPos;

    private void Start()
    {
        localStartPos = transform.localPosition;
    }

    private void Update()
    {
        if (shakeDuration > 0)
        {
            transform.localPosition = localStartPos + Random.insideUnitSphere * shakeAmount;
            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
    }

    public void Shake(float duration, float amount, float decreaseFactor)
    {
        Debug.Log("Camera Shake Start");

        shakeDuration = duration;
        shakeAmount = amount;
        this.decreaseFactor = decreaseFactor;
    }
}