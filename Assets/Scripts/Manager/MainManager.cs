using System.Collections;
using UnityEngine;

public enum GameState
{
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

    private GameState gameState;

    private MetalBar[] bars;

    private void Awake()
    {
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
                playerCanvas.SetActive(false);
                gameplayCamera.SetActive(false);

                menuCanvas.SetActive(true);
                menuCamera.SetActive(true);
                lostCanvas.SetActive(false);
                break;
            case GameState.PLAY:
                menuCanvas.SetActive(false);
                menuCamera.SetActive(false);

                player.enabled = true;
                playerCanvas.SetActive(true);
                gameplayCamera.SetActive(true);
                lostCanvas.SetActive(false);
                break;

            case GameState.WON:

                player.enabled = false;
                wonCanvas.SetActive(true);
                break;

            case GameState.LOST:

                player.enabled = false;

                player.meshRend.enabled = false;
                player.currentSpeed = 0;

                player.rigid.velocity = Vector3.zero;

                GameObject explPlayer = Instantiate(explosionPlayer);
                explPlayer.transform.position = player.transform.position;



                Rigidbody rigid = explPlayer.GetComponentInChildren<Rigidbody>();
                rigid.transform.DetachChildren();
                rigid.AddExplosionForce(14f, explosionPlayer.transform.position - Vector3.forward * 3f, 3f);


                playerCanvas.SetActive(false);
                float delay = 3;
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

}