using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Assignments")]
    public Rigidbody rigid;
    public PlayerCamera playerCamera;
    public Transform startPoint;

    [Header("Handling")]
    public AnimationCurve handlingCurve;
    public float minHandling;
    public float maxHandling;
    public float decreaseFactor;


    [Header("Speed")]
    public float currentSpeed;
    public float accelDiv;
    public float boostDiv;
    public float minSpeed;
    public float maxSpeed;

    [Header("Speed Threshold")]
    public float killSpeed;

    [Header("Brake")]
    public float breakMultiplier;
    public float brakeGraceDuration;
    public float brakeGraceModifier;

    [Header("Ressource")]
    public float movementDecreaseFactor;
    public float breakDecreaseFactor;
    public float metalBarRessource;

    //PRIVATE
    private Vector3 frameInput;

    private float lastLeftDownTime;
    private float lastRightDownTime;
    private float lastBrakeTime;

    private Coroutine startRoutine;
    private float startCountdown;
    private MainManager manager;

    //EVENTS
    public static event Action OnLeftKey = delegate { };
    public static event Action OnRightKey = delegate { };
    public static event Action OnBrakeKey = delegate { };

    //PROPERTIES
    public bool IsJumping { get; set; }
    public bool IsOnSpeedbooster { get; set; }

    private void Start()
    {
        rigid.velocity = new Vector3(0f, 0f, 10f);

        MetalBar.OnMetalBarPickup += () =>
        {
            metalBarRessource += 0.5f;
            metalBarRessource = Mathf.Clamp01(metalBarRessource);
        };
        manager = FindObjectOfType<MainManager>();
        startRoutine = StartCoroutine(StartSpeedUp());
    }

    private void Update()
    {
        if (startRoutine != null) return;
        if (IsJumping) return;

        if (currentSpeed > killSpeed)
        {
            manager.SwitchGameplay(GameState.LOST);
            rigid.velocity = Vector3.zero;
            rigid.useGravity = false;
            return;
        }

        frameInput = Vector3.zero;

        float t = Remapper.Remap(currentSpeed, minSpeed, maxSpeed, 0, 1);
        float handling = Mathf.Lerp(minHandling, maxHandling, handlingCurve.Evaluate(t));

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

        //Speedbooster
        if (IsOnSpeedbooster)
        {
            currentSpeed += Time.deltaTime / boostDiv;
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

        if (IsOnSpeedbooster)
            rigid.velocity += transform.up * -2f;
    }

    public void Reset()
    {
        currentSpeed = 0;
        manager.SwitchGameplay(GameState.PLAY);
        rigid.useGravity = true;
        transform.rotation = Quaternion.identity;
        transform.position = startPoint.position;
        startRoutine = StartCoroutine(StartSpeedUp());
        rigid.constraints = RigidbodyConstraints.FreezeRotationY & RigidbodyConstraints.FreezeRotationZ;
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

    //private void OnGUI()
    //{
    //    GUI.Label(new Rect(0, 0, 200, 50), "Speed:" + rigid.velocity.z);
    //    GUI.Label(new Rect(0, 100, 200, 50), "Metal Bar Ressource:" + metalBarRessource);
    //    GUI.Label(new Rect(0, 200, 200, 50), "IsSpeedBooster:" + IsOnSpeedbooster);

    //    if (startRoutine != null)
    //    {
    //        GUI.Label(new Rect(960, 540, 100, 50), "Countdown   " + startCountdown);
    //    }
    //}
}