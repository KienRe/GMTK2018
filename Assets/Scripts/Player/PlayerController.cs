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
    public float accelDiv = 8f;
    public float minSpeed = 1f;
    public float maxSpeed = 4f;

    [Header("Brake")]
    public float breakMultiplier = 2f;
    public float brakeGraceDuration = 1f;
    public float brakeGraceModifier = 4f;

    [Header("Ressource")]
    public float movementDecreaseFactor;
    public float breakDecreaseFactor;
    public float metalBarRessource = 1.0f;

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

        MetalBar.OnMetalBarPickup += () => 
        {
            metalBarRessource += 0.5f;
            metalBarRessource = Mathf.Clamp01(metalBarRessource);
        };

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
        if (metalBarRessource > 0.0f && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)))
        {
            frameInput += Vector3.left * handling * Mathf.Lerp(1, 0, lastLeftDownTime / (Time.time * decreaseFactor));

            metalBarRessource -= Time.deltaTime * movementDecreaseFactor;
            OnLeftKey();
        }
        else if (metalBarRessource > 0.0f && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)))
        {
            frameInput += Vector3.right * handling * Mathf.Lerp(1, 0, lastLeftDownTime / (Time.time * decreaseFactor));

            metalBarRessource -= Time.deltaTime * movementDecreaseFactor;
            OnRightKey();
        }


        //Braking or Acceleration
        if (metalBarRessource > 0.0f && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)))
        {
            OnBrakeKey();
            lastBrakeTime = Time.time;

            currentSpeed -= Time.deltaTime * breakMultiplier;
            metalBarRessource -= Time.deltaTime * breakDecreaseFactor;
        }
        else
        {
            //When braking the acceleration is slowed for brakeGraceDuration
            if (lastBrakeTime + brakeGraceDuration > Time.time)
            {
                Debug.Log("Grace Brake Time");

                currentSpeed += Time.deltaTime * brakeGraceModifier;
            }
            //Normal acceleration
            else
            {
                currentSpeed += Time.deltaTime / accelDiv;
            }
        }

        currentSpeed = Mathf.Clamp(currentSpeed, minSpeed, maxSpeed);

        rigid.velocity += frameInput;
        rigid.velocity = new Vector3(rigid.velocity.x, rigid.velocity.y, currentSpeed);
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

        currentSpeed = minSpeed;
        startRoutine = null;
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 200, 50), "Speed:" + rigid.velocity.z);
        GUI.Label(new Rect(0, 100, 200, 50), "Metal Bar Ressource:" + metalBarRessource);

        if (startRoutine != null)
        {
            GUI.Label(new Rect(960, 540, 100, 50), "Countdown   " + startCountdown);
        }
    }
}