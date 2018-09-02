using System.Collections;
using UnityEngine;

public enum GameState
{
    NONE,
    MENU,
    PLAY,
    WON,
    LOST
}

public class MainManager : MonoBehaviour
{
    [Header("Settings")]
    public GameState defaultGameState;

    [Header("Scene Assignmnets")]
    public PlayerController player;
    public GameObject playerCanvas;
    public GameObject menuCanvas;
    public GameObject lostCanvas;
    public GameObject wonCanvas;
    public GameObject explosionPlayer;

    public GameObject gameplayCamera;
    public GameObject menuCamera;

    [Header("Camera Transform")]
    public Transform menuCameraTransform;
    public Transform gameplayCameraTransform;

    [Header("Audio")]
    public AudioSource themeAudio;

    private GameState gameState = GameState.NONE;

    private MetalBar[] bars;
    public AudioSource explosionAudio;

    private void Awake()
    {
        themeAudio.Play();

        menuCamera.transform.position = menuCameraTransform.position;
        gameplayCamera.transform.position = gameplayCameraTransform.position;

        SwitchGameplay(defaultGameState);
        bars = FindObjectsOfType<MetalBar>();
    }

    public void SwitchGameplay(GameState state)
    {
        if (state == gameState) return;

        gameState = state;

        switch (state)
        {
            case GameState.MENU:
                player.enabled = false;
                player.rigid.constraints = RigidbodyConstraints.FreezeAll;
                playerCanvas.SetActive(false);
                gameplayCamera.SetActive(false);

                menuCanvas.SetActive(true);
                menuCamera.SetActive(true);
                lostCanvas.SetActive(false);
                break;
            case GameState.PLAY:
                player.rigid.constraints = RigidbodyConstraints.None;
                player.rigid.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

                menuCanvas.SetActive(false);
                menuCamera.SetActive(false);

                player.enabled = true;
                playerCanvas.SetActive(true);
                gameplayCamera.SetActive(true);
                lostCanvas.SetActive(false);
                break;

            case GameState.WON:

                player.enabled = false;
                playerCanvas.SetActive(false);
                float delay = 0;
                StartCoroutine(ActivateWinCanvas(true, delay));
                StartCoroutine(ResetGamePlay(delay + wonCanvas.GetComponentInChildren<ImageLerp>().duration));

                break;

            case GameState.LOST:

                player.enabled = false;

                player.meshRend.enabled = false;
                player.currentSpeed = 0;

                player.rigid.velocity = Vector3.zero;
                player.metalbar.SetActive(false);
                GameObject explPlayer = Instantiate(explosionPlayer);
                explPlayer.transform.position = player.transform.position;

                Rigidbody rigid = explPlayer.GetComponentInChildren<Rigidbody>();
                rigid.transform.DetachChildren();
                rigid.AddExplosionForce(14f, explosionPlayer.transform.position - Vector3.forward * 3f, 3f);

                playerCanvas.SetActive(false);
                delay = 3;
                explosionAudio.Play();
                StartCoroutine(ActivateLostCanvas(true, delay));
                StartCoroutine(ResetGamePlay(delay + lostCanvas.GetComponentInChildren<ImageLerp>().duration));
                break;

            default:
                break;
        }
    }

    public void OnStartButtonClick()
    {
        StartCoroutine(SwitchToGameplay());
    }

    public void OnExitButtonClick()
    {
        Application.Quit();
    }

    private IEnumerator ResetGamePlay(float delay)
    {
        float timer = 0;
        while (timer < delay)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        SwitchGameplay(GameState.PLAY);

        foreach (var item in bars)
        {
            item.gameObject.SetActive(true);
        }
        player.Reset();
    }

    private IEnumerator SwitchToGameplay()
    {
        float timeForEffect = 2f;
        float elapsed = 0f;

        menuCanvas.SetActive(false);

        while (elapsed < timeForEffect)
        {
            menuCamera.transform.position = Easing.DoEasing(EasingTypes.Linear, menuCamera.transform.position, gameplayCameraTransform.position, elapsed / timeForEffect);
            menuCamera.transform.rotation = Quaternion.Euler(Easing.DoEasing(EasingTypes.Linear, menuCamera.transform.rotation.eulerAngles, gameplayCamera.transform.rotation.eulerAngles, elapsed / timeForEffect));
            elapsed += Time.deltaTime;
            yield return null;
        }

        player.rigid.constraints = RigidbodyConstraints.None;
        player.rigid.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        menuCamera.SetActive(false);
        player.enabled = true;
        playerCanvas.SetActive(true);
        gameplayCamera.SetActive(true);
    }

    private IEnumerator ActivateLostCanvas(bool isActive, float delay)
    {
        float timer = 0;

        while (timer < delay)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        lostCanvas.SetActive(isActive);
        if (isActive)
            lostCanvas.GetComponentInChildren<ImageLerp>().StartCoroutine(lostCanvas.GetComponentInChildren<ImageLerp>().PanelFlash());
    }

    private IEnumerator ActivateWinCanvas(bool isActive, float delay)
    {
        float timer = 0;

        while (timer < delay)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        wonCanvas.SetActive(isActive);
        if (isActive)
            wonCanvas.GetComponentInChildren<ImageLerp>().StartCoroutine(wonCanvas.GetComponentInChildren<ImageLerp>().PanelFlash());
    }


}