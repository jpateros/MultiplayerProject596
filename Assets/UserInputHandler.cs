using UnityEngine;

public class UserInputHandler : MonoBehaviour
{
    CarController carController;
    Vector2 inputVector;
    Vector3 playerPosition;
    bool allowInput; // Flag to control user input

    void Awake()
    {
        carController = GetComponent<CarController>();
        allowInput = true;
    }

    void Update()
    {

        if (allowInput)
        {
            // Get input from the horizontal and vertical axes
            inputVector.x = Input.GetAxis("Horizontal");
            inputVector.y = Input.GetAxis("Vertical");

            // Set the input vector in the car controller
            carController.SetInputVector(inputVector);

            // Update the player position
            playerPosition = transform.position;
        }
        Debug.Log("Waiting to be able to run");
    }

    public Vector3 GetPlayerPosition()
    {
        return playerPosition;
    }

    // Method to enable user input after the countdown
    public void EnableInput()
    {
        allowInput = true;
        Debug.Log("Enable run");
        Debug.Log(allowInput);
    }
}