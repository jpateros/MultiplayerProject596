using Unity.Multiplayer.Samples.Utilities.ClientAuthority;
using Unity.Netcode;
using UnityEngine;

public class StartNetwork : MonoBehaviour
{
    public Transform clientSpawnPoint; // Assign this in the Inspector to a GameObject representing the desired spawn point for the client

    // Start is called before the first frame update
    public void StartServer()
    {
        NetworkManager.Singleton.StartServer();
    }

    public void StartClient()
    {
        NetworkManager.Singleton.StartClient();
    }

    public void StartHost()
    {
        NetworkManager.Singleton.StartHost();
    }

    
}

    
