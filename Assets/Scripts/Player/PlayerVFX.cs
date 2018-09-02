using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVFX : MonoBehaviour
{
    [Header("Scene Assignmnets")]
    public ParticleSystem leftSideSparks;
    public ParticleSystem rightSideSparks;
    public ParticleSystem landingSparks;
    public ParticleSystem crowbarSparksLeft;
    public ParticleSystem crowbarSparksRight;
    public ParticleSystem breakSparks;

    public bool IsLeftSideCol { get; set; }
    public bool IsRightSideCol { get; set; }


    private void Update()
    {
        if (IsLeftSideCol)
            leftSideSparks.Play();

        if (IsRightSideCol)
            rightSideSparks.Play();
    }
}
