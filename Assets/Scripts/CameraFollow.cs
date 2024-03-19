using Unity.Netcode;
using UnityEngine;

/// <summary>
/// This class use unity netcode to ensure that the camer will ownly follow the owner of the script so each client can have own camer view.
/// </summary>
public class CameraFollow : NetworkBehaviour
{
    public GameObject MainCamera;
    public Vector3 offset;

    //Camera should only be set active if the player is the owner of the object
    public override void OnNetworkSpawn()
    { 
        MainCamera.SetActive(IsOwner);
        base.OnNetworkSpawn(); 
    }

    //Keep moving the camera on top of the players position to follow them
    void Update()
    {
        MainCamera.transform.position = transform.position + new Vector3(0, 0, -10);
    }
}
