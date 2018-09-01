using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Assignments")]
    public Rigidbody rigid;

    [Header("Handling")]
    public float handling;
    public float decreaseFactor = 1;
    public AnimationCurve handlingModifierCurve;

    [Header("Speed")]
    public float currentSpeed;
    public float minSpeed = 1f;
    public float maxSpeed = 4f;

    public AnimationCurve speedCurve;

    [Header("Brake")]
    public float brakeGraceDuration = 1f;
    public float brakeGraceModifier = 4f;

    //PRIVATE
    private Vector3 frameInput;

    private float lastLeftDownTime;
    private float lastRightDownTime;
    private float lastBrakeTime;

    private Coroutine startRoutine;
    private float startCountdown;

    //EVENTS
    public static event Action OnLeftKey = delegate { };
    public static event Action OnRightKey = delegate { };
    public static event Action OnBrakeKey = delegate { };

    private void Start()
    {
        rigid.velocity = new Vector3(0f, 0f, 10f);

        startRoutine = StartCoroutine(StartSpeedUp());
    }

    private void Update()
    {
        if (startRoutine != null) return;

        frameInput = Vector3.zero;

        //Button Down Timer
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            lastLeftDownTime = Time.time;
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            lastRightDownTime = Time.time;
        }

        //Left Right Movement
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            frameInput += Vector3.left * handling * Mathf.Lerp(1, 0, lastLeftDownTime / (Time.time * decreaseFactor));
            OnLeftKey();
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            frameInput += Vector3.right * handling * Mathf.Lerp(1, 0, lastLeftDownTime / (Time.time * decreaseFactor));
            OnRightKey();
        }

        //Braking or Acceleration
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            OnBrakeKey();
            lastBrakeTime = Time.time;
        }
        else
        {


            //speedCurve.Evaluate()

            if(lastBrakeTime + brakeGraceDuration > Time.time)
            {

            }
        }

        rigid.velocity += frameInput;
    }

    private IEnumerator StartSpeedUp()
    {
        float timeForEffect = 3f;
        startCountdown = 0f;

        while (startCountdown < timeForEffect)
        {
            rigid.velocity = transform.forward * Mathf.Lerp(0f, minSpeed, startCountdown / timeForEffect);
            startCountdown += Time.deltaTime;
            yield return null;
        }

        startRoutine = null;
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 200, 50), "Speed:" + rigid.velocity.z);

        if (startRoutine != null)
        {
            GUI.Label(new Rect(960, 540, 100, 50), "Countdown   " + startCountdown);
        }
    }
}