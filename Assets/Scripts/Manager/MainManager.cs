using System.Collections;
using UnityEngine;

public enum GameState
{
    MENU,
    PLAYING,
    WON
}

public class MainManager : MonoBehaviour
{
    [Header("Settings")]
    public GameState defaultGameState;

    [Header("Scene Assignmnets")]
    public PlayerController player;
    public GameObject playerCanvas;
    public GameObject menuCanvas;

    public GameObject gameplayCamera;
    public GameObject menuCamera;

    [Header("Camera Transform")]
    public Transform menuCameraTransform;
    public Transform gameplayCameraTransform;

    private GameState gameState;

    private void Awake()
    {
        gameState = defaultGameState;

        menuCamera.transform.position = menuCameraTransform.position;
        gameplayCamera.transform.position = gameplayCameraTransform.position;

        switch (gameState)
        {
            case GameState.MENU:
                player.enabled = false;
                playerCanvas.SetActive(false);
                gameplayCamera.SetActive(false);

                menuCanvas.SetActive(true);
                menuCamera.SetActive(true);
                break;
            case GameState.PLAYING:
                menuCanvas.SetActive(false);
                menuCamera.SetActive(false);

                player.enabled = true;
                playerCanvas.SetActive(true);
                gameplayCamera.SetActive(true);
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
}