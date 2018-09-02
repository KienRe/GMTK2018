using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVFX : MonoBehaviour
{
    private PlayerController player;

    [Header("Scene Assignmnets")]
    public ParticleSystem leftSideSparks;
    public ParticleSystem rightSideSparks;
    public ParticleSystem landingSparks;
    public ParticleSystem crowbarSparksLeft;
    public ParticleSystem crowbarSparksRight;
    public ParticleSystem breakSparks;

    public bool IsLeftSideCol { get; set; }
    public bool IsRightSideCol { get; set; }

    private void Start()
    {
        player = GetComponent<PlayerController>();
    }


    private void Update()
    {
        if (IsLeftSideCol)
        {
            leftSideSparks.Play();

            if (!player.collisionAudio.isPlaying)
                player.collisionAudio.Play();
        }


        if (IsRightSideCol)
        {
            rightSideSparks.Play();

            if (!player.collisionAudio.isPlaying)
                player.collisionAudio.Play();
        }
    }
}
