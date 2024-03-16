using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class UserInputHandler : NetworkBehaviour
{

    CarController carController;
    Vector3 playerPosition;
    void Awake()
    {
        carController = GetComponent<CarController>();
       
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputVector = Vector2.zero;

        inputVector.x = Input.GetAxis("Horizontal");
        //made to ensure forward key is forward lols
        inputVector.y = Input.GetAxis("Vertical");

        carController.SetInputVector(inputVector);
        playerPosition = transform.position;
    }

    public Vector3 GetPlayerPosition()
    {
        return playerPosition;
    }
}
