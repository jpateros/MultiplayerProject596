using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class UserInputHandler : NetworkBehaviour
{

    CarController carController;


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
        inputVector.y = Input.GetAxis("Vertical");

        carController.SetInputVector(inputVector);
    }
}
