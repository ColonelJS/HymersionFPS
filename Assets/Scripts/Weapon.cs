using UnityEngine;
using Fusion;

public class Weapon : NetworkBehaviour
{
    [SerializeField] Bullet bulletPrefab;
    [SerializeField] Transform bulletSpawnPoint;

    [SerializeField] float _bulletSpeed = 2400f;

    [Networked] private TickTimer delay { get; set; }

    public void Shoot(Vector3 direction)
    {
        Bullet newBullet = NetworkManager.Instance.GetNetworkRunner().Spawn(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation, Object.InputAuthority);

        newBullet.GetRigidbody().AddForce(direction, ForceMode.Impulse);
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData inputData))
        {
            if (delay.ExpiredOrNotRunning(Runner))
            {
                if (inputData.weaponShoot && HasStateAuthority)
                {
                    Shoot(bulletSpawnPoint.forward * _bulletSpeed * Runner.DeltaTime);
                    delay = TickTimer.CreateFromSeconds(Runner, 0.1f);
                }
            }
        }
    }
}
