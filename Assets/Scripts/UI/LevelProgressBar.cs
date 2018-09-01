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

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        distance = endPos.position.x - startPos.position.x - startPos.rect.width - endPos.rect.width;
    }

    void Update()
    {
        float progess = Remapper.Remap(trackDistance, 0, 100, startPos.position.x, endPos.position.x);
        playerIcon.position = new Vector3(progess, playerIcon.position.y, playerIcon.position.z);

    }
}
