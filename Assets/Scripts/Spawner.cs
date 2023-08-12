using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] NetworkObject player;

    public NetworkObject SpawnPlayer(PlayerRef _player)
    {
        return NetworkManager._networkRunner.Spawn(player, Vector3.zero, Quaternion.identity, _player);
    }
}
