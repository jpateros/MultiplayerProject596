using Unity.Netcode;
using UnityEngine;

/// <summary>
/// This class contains the Netcode methods to start the Server, Client, and Host based on button click
/// </summary>
public class StartNetwork : MonoBehaviour
{
    
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