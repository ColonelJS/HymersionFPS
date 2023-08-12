using UnityEngine;
using Fusion;
using TMPro;

public class Player : NetworkBehaviour
{
    [SerializeField] private NetworkObject _networkObject;
    [SerializeField] private TextMeshProUGUI _playerNameText;

    [SerializeField] private PlayerMovements playerMovements;

    public PlayerMovements GetPlayerMovements() => playerMovements;

    public override void Spawned()
    {
        if(HasStateAuthority)
            Cursor.visible = false;
    }
}
