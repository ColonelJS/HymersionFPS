using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System.Collections.Generic;
using System;

public class NetworkManager : MonoBehaviour, INetworkRunnerCallbacks
{
    public static NetworkManager Instance;

    NetworkRunner _networkRunner;
    [SerializeField] Player playerPrefab;
    PlayerSpawn playerSpawns;
    HUD hud;

    Dictionary<PlayerRef, Player> _listPlayers = new Dictionary<PlayerRef, Player>();

    private void Awake()
    {
        // Singleton
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    /// <summary>
    /// Called for hosting a unique session named MasterSession
    /// </summary>
    public async void HostSession()
    {
        if (_networkRunner == null)
        {
            _networkRunner = gameObject.AddComponent<NetworkRunner>();
        }

        await _networkRunner.StartGame(new StartGameArgs
        {
            SessionName = "MasterSession",
            GameMode = GameMode.Host,
            Scene = 1,
            PlayerCount = 10,
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        });
    }

    /// <summary>
    /// Called for joining the unique session named MasterSession
    /// </summary>
    public async void JoinSession()
    {
        if (_networkRunner == null)
        {
            _networkRunner = gameObject.AddComponent<NetworkRunner>();
        }

        await _networkRunner.StartGame(new StartGameArgs
        {
            SessionName = "MasterSession",
            GameMode = GameMode.Client,
            Scene = 1,
            PlayerCount = 10,
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        });
    }

    public void OnConnectedToServer(NetworkRunner runner)
    {

    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {

    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
        
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
        
    }

    public void OnDisconnectedFromServer(NetworkRunner runner)
    {
        
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
        
    }


    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        if (runner.GetPlayerObject(runner.LocalPlayer) && runner.GetPlayerObject(runner.LocalPlayer).HasInputAuthority)
        {
            NetworkInputData data = runner.GetPlayerObject(runner.LocalPlayer).GetComponent<Player>().GetPlayerMovements().GetPlayerInputs();
            input.Set(data);
        }
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {

    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        if (runner.IsServer)
        {
            Player netplayerObj = _networkRunner.Spawn(playerPrefab, playerSpawns.GetRandomSpawnPoint().position, Quaternion.identity, player);
            if(netplayerObj != null)
            {
                _listPlayers.Add(player, netplayerObj);
                runner.SetPlayerObject(player, netplayerObj.GetComponent<NetworkObject>());
            }
        }
    }


    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        if (_listPlayers.TryGetValue(player, out Player networkPlayerObj))
        {
            _networkRunner.Despawn(networkPlayerObj.GetComponent<NetworkObject>());
            _listPlayers.Remove(player);
        }
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
    {
        
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
        //Retreive the playerSpawns object of the scene who contain all the spawn point
        playerSpawns = GameObject.FindGameObjectWithTag("PlayerSpawn").GetComponent<PlayerSpawn>();
        hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUD>();
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
        
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
        
    }

    public PlayerSpawn GetPlayerSpawns() => playerSpawns;
    public NetworkRunner GetNetworkRunner() => _networkRunner;

    public HUD GetHUD() => hud;
}
