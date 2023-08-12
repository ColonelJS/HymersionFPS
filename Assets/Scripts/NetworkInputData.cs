using Fusion;
using UnityEngine;

public struct NetworkInputData : INetworkInput
{
    public Vector2 direction;
    public Vector3 rotation;
    public bool weaponShoot;
}