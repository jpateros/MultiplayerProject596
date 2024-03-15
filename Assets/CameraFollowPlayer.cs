/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class AssignCamera : NetworkBehaviour
{
    public Transform Player;
    public GameObject Camera;

    // Start is called before the first frame update
    void Start()
    {
        //Player = GameObject.FindGameObjectWithTag("Player").transform;
        Player = NetworkManager.LocalClient.PlayerObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner)
        {
            Camera.SetActive(false); //The cam itself
        }
    }

}*/