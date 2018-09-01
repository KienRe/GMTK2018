using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelProgressBar : MonoBehaviour
{
    public RectTransform startPos;
    public RectTransform endPos;
    public RectTransform playerIcon;
    public Transform trackStart;
    public Transform trackEnd;

    private PlayerController player;

    private float trackDistance;
    private float distance;
    private bool isStarted = false;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        trackDistance = Vector3.Distance(trackStart.position, trackEnd.position);
        distance = endPos.position.x - startPos.position.x - startPos.rect.width - endPos.rect.width;
    }

    void Update()
    {
        float playerDistance = Vector3.Distance(player.transform.position, trackStart.position);
        //Prewarm Phase
        if (playerDistance < 0.5f && !isStarted)
        {
            isStarted = true;
        }
        
        if (isStarted)
        {
            playerDistance /= trackDistance;
            playerDistance = Mathf.Clamp01(playerDistance);
            float progess = Remapper.Remap(playerDistance, 0, 1, startPos.position.x + startPos.rect.width/2, endPos.position.x - endPos.rect.width/2);
            playerIcon.position = new Vector3(progess, playerIcon.position.y, playerIcon.position.z);
        }

    }
}
