using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] private List<Transform> listSpawnPoint;

    /// <summary>
    /// Used for getting a random spawn point when the player character is spawned
    /// </summary>
    /// <returns></returns>
    public Transform GetRandomSpawnPoint()
    {
        return listSpawnPoint[Random.Range(0, listSpawnPoint.Count)];
    }
}
