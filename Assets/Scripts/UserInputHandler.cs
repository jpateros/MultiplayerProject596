using Unity.Netcode;
using UnityEngine;

/// <summary>
/// This class takes user input from the players for infromation used by CarController Script.
/// </summary>
public class UserInputHandler : NetworkBehaviour
{
    CarController carController;
    Vector2 inputVector;
    Vector3 playerPosition;
    bool allowInput; 
    public AudioSource horn;

    void Awake()
    {
        carController = GetComponent<CarController>();
        allowInput = true;
    }

    //Constantly get the user input for the 
    void Update()
    {
        //Netwroking Check: only control user input for the player owning script 
        if (!IsOwner) return;

        //Allow both players to start once the coiuntdown has finished
        if (allowInput)
        {
            // Get input from the horizontal and vertical axes update Car Controller
            inputVector.x = Input.GetAxis("Horizontal");
            inputVector.y = Input.GetAxis("Vertical");
            carController.SetInputVector(inputVector);

            // Update the player position
            playerPosition = transform.position;

            //Play horn sound when the "h" key is pressed
            if (Input.GetKeyDown(KeyCode.H))
            {
                horn.Play();
            }
        }
        Debug.Log("Waiting to be able to run");
    }

    public Vector3 GetPlayerPosition()
    {
        return playerPosition;
    }

    // Callback method will enable user input after the countdown has finished
    public void EnableInput()
    {
        allowInput = true;
        Debug.Log("Enable run");
        Debug.Log(allowInput);
    }
}
