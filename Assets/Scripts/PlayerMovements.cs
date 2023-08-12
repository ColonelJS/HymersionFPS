using Fusion;
using UnityEngine;

public class PlayerMovements : NetworkBehaviour
{
    [SerializeField] private SelfCamera playerCamera;
    [SerializeField] private NetworkCharacterControllerPrototype _charController;

    [SerializeField] private float _sensibility = 1f;
    [SerializeField] private float _moveSpeed = 10f;
    float cameraRotationX = 0;

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData inputData))
        {
            inputData.direction.Normalize();

            //Character movements
            Vector3 direction = transform.forward * inputData.direction.y + transform.right * inputData.direction.x;

            //Update camera movements
            if (playerCamera.GetCamera().isActiveAndEnabled)
            {
                cameraRotationX += inputData.rotation.y * _sensibility * _charController.rotationSpeed * Runner.DeltaTime;
                cameraRotationX = Mathf.Clamp(cameraRotationX, -80, 80);
                playerCamera.GetCamera().transform.localRotation = Quaternion.Euler(cameraRotationX, 0, 0);
            }

            //Character rotations
            _charController.Rotate(_sensibility * inputData.rotation.x * Runner.DeltaTime);

            _charController.Move(_moveSpeed * direction * Runner.DeltaTime);
        }
    }

    /// <summary>
    /// Retreive all player keyboard & mouse inputs for movements
    /// </summary>
    /// <returns></returns>
    public NetworkInputData GetPlayerInputs()
    {
        var data = new NetworkInputData();

        data.direction.x = Input.GetAxis("Horizontal");
        data.direction.y = Input.GetAxis("Vertical");
        data.rotation.x = Input.GetAxis("Mouse X");
        data.rotation.y = -Input.GetAxis("Mouse Y");

        data.weaponShoot = Input.GetButton("Fire1");

        return data;
    }
}
