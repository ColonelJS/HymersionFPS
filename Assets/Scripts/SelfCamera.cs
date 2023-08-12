using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class SelfCamera : NetworkBehaviour
{
    [SerializeField] Camera selfCamera;

    void Start()
    {
        if(!HasInputAuthority)
            selfCamera.enabled = false;
    }

    public Camera GetCamera() => selfCamera;
}
