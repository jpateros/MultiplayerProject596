using Unity.Netcode;
using UnityEngine;

/// <summary>
/// This class controls the simualted car physics based on input given from UserInputHandler Script/
/// </summary>
public class CarController : NetworkBehaviour
{
    private float accelerationFactor = 5.0f;
    public float turnFactor = 3.5f;
    public float driftFactor = 0.95f;
    public float maxSpeed = 20;
    private float velocityVsUp = 0;
    private float accelerationInput = 0;
    private float steeringInput = 0;
    private float rotationAngle = 0;

    Rigidbody2D carRigidBody2D;
    
    //When script instance is loaded get the carRigiBody component
    void Awake()
    {
        carRigidBody2D = GetComponent<Rigidbody2D>();
    }
    
    void FixedUpdate()
    {
       //Networking check: ensure that we only have the owner of the script controlling movement of player
       if (!IsOwner) return;

        ApplyEngineForce();
        KillOrthogonalVelocity();
        ApplySteering();
    }

    void ApplyEngineForce()
    {
        velocityVsUp = Vector2.Dot(transform.up, carRigidBody2D.velocity);
        //cant go faster thanmax speed in forward direction
        if (velocityVsUp > maxSpeed && accelerationInput > 0)
            return;
        //cant go faster than half max speed in backward direction
        if (velocityVsUp < - maxSpeed * 0.5f && accelerationInput < 0)
            return;

        //should not accelerate in any direction
        if (carRigidBody2D.velocity.sqrMagnitude > maxSpeed * maxSpeed && accelerationInput > 0)
            return;

        //apply drag when not moving t
        if (accelerationInput == 0)
        {
            carRigidBody2D.drag = Mathf.Lerp(carRigidBody2D.drag, 3.0f, Time.fixedDeltaTime * 3);
    
        }
        else
        {
            carRigidBody2D.drag = 0;
        }
        Vector2 engineForceVector = transform.up * accelerationInput * accelerationFactor;
        //apply force and push the car forward
        carRigidBody2D.AddForce(engineForceVector, ForceMode2D.Force);
    }

    void ApplySteering()
    {
        //Limits car ability to turn when moving slowly
        float minSpeedBeforeAllowTurnFactor = (carRigidBody2D.velocity.magnitude / 8);
        minSpeedBeforeAllowTurnFactor = Mathf.Clamp01(minSpeedBeforeAllowTurnFactor);
        rotationAngle -= steeringInput * turnFactor * minSpeedBeforeAllowTurnFactor;
        carRigidBody2D.MoveRotation(rotationAngle);
    }

    void KillOrthogonalVelocity()
    {
        //Car physics mimic a real car with adding car tire friction for drifting
        Vector2 forwardVelocity = transform.up * Vector2.Dot(carRigidBody2D.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(carRigidBody2D.velocity, transform.right);

        carRigidBody2D.velocity = forwardVelocity + rightVelocity * driftFactor;
    }
    float GetLateralVelocity()
    {
        return Vector2.Dot(transform.right, carRigidBody2D.velocity);
    }

    //This helper method is used to determine SFX and graphics (like drifitng) for the car
    public bool IsTireScreeching(out float laterVelocity, out bool isBraking)
    {
        laterVelocity = GetLateralVelocity();
        isBraking = false;

        //Scenario 1: Moving forward and hitting brakes 
        if (accelerationInput < 0 && velocityVsUp > 0)
        {
            isBraking = true;
            return true;
        }
        //Scenario 2: A lot of lateral movmement (drifting)
        if (Mathf.Abs(GetLateralVelocity()) > 1.0f)
            return true;

        return false;
        
    }

    public void SetInputVector(Vector2 inputVector)
    {
        steeringInput = inputVector.x;
        accelerationInput = inputVector.y;
    }

    public float GetVelocityMagnitude()
    {
        return carRigidBody2D.velocity.magnitude;
    }
}
