using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelProgressBar : MonoBehaviour
{

    public RectTransform startPos;
    public RectTransform endPos;
    public RectTransform player;

    [Range(0f, 100f)]
    public float value;
    private float distance;

    private void Start()
    {
        distance = endPos.position.x - startPos.position.x - startPos.rect.width - endPos.rect.width;
    }

    void Update()
    {
        float progess = Remapper.Remap(value, 0, 100, startPos.position.x, endPos.position.x);
        player.position = new Vector3(progess, player.position.y, player.position.z);

    }
}
