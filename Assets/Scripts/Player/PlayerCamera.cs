using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerCamera : MonoBehaviour
{
    [Header("FOV Settings")]
    public AnimationCurve fovCurve;
    public float minFOV;
    public float maxFOV;

    [Header("Motion Blur")]
    public AnimationCurve motionBlurCurve;
    public float minShutterAngle;
    public float maxShutterAngle;

    [Header("Camera Shake")]
    public AnimationCurve shakeCurve;
    public float minShake;
    public float maxShake;

    [Header("Scene Assignments")]
    public PlayerController player;
    public PostProcessVolume volume;

    //Camera Shake
    private float shakeDuration = 0f;
    private float shakeAmount = 0f;
    private float decreaseFactor = 0f;

    private Vector3 localStartPos;
    private Camera cameraComponent;
    private MotionBlur motionBlur;

    private void Start()
    {
        localStartPos = transform.localPosition;
        cameraComponent = GetComponent<Camera>();
        volume.profile.TryGetSettings<MotionBlur>(out motionBlur);
    }

    private void Update()
    {
        float t = Remapper.Remap(player.currentSpeed, player.minSpeed, player.maxSpeed, 0, 1);

        //Camera FoV
        cameraComponent.fieldOfView = Mathf.Lerp(minFOV, maxFOV, fovCurve.Evaluate(t));

        //Camera MotionBlur
        motionBlur.shutterAngle.value = Mathf.Lerp(minShutterAngle, maxShutterAngle, motionBlurCurve.Evaluate(t));

        //Camera Shake
        transform.localPosition = localStartPos + Random.insideUnitSphere * Mathf.Lerp(minShake, maxShake, shakeCurve.Evaluate(t));
    }
}