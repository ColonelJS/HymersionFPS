using Fusion;
using UnityEngine;

public class Bullet : NetworkBehaviour
{
    [SerializeField] Rigidbody _rigidbody;

    public Rigidbody GetRigidbody() => _rigidbody;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            other.GetComponent<Player>().RemovePlayerLifePoint(10);
    }
}
