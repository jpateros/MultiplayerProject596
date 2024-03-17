using Unity.Netcode;
using UnityEngine;

public class CameraFollow : NetworkBehaviour
{
 
    public GameObject MainCamera;
    public Vector3 offset;
    //camera should only be set active if the player is the owner of the object
    public override void OnNetworkSpawn()
    { // This is basically a Start method
        MainCamera.SetActive(IsOwner);
        base.OnNetworkSpawn(); // Not sure if this is needed though, but good to have it.
    }


    void Update()
    {
        MainCamera.transform.position = transform.position + new Vector3(0, 0, -10);
    }
}
