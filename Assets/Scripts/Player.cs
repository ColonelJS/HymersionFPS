using UnityEngine;
using Fusion;
using TMPro;

public class Player : NetworkBehaviour
{
    [SerializeField] private NetworkObject _networkObject;

    [SerializeField] private PlayerMovements playerMovements;

    public PlayerMovements GetPlayerMovements() => playerMovements;

    [SerializeField] private int _baseLifePoint = 100;
    private int _lifePoint;

    public override void Spawned()
    {
        _lifePoint = _baseLifePoint;
        UpdateHudLifePoint();
        if (HasStateAuthority)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void RemovePlayerLifePoint(int lifePoint)
    {
        _lifePoint -= lifePoint;
        UpdateHudLifePoint();
        if (_lifePoint <= 0)
            Respawn();
    }

    public void Respawn()
    {
        transform.position = NetworkManager.Instance.GetPlayerSpawns().GetRandomSpawnPoint().position;
        _lifePoint = _baseLifePoint;
    }

    private void UpdateHudLifePoint() => NetworkManager.Instance.GetHUD().GetLifeLeftText().text = _lifePoint.ToString();
}
