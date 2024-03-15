using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private float accelerationFactor = 5.0f;
    public float turnFactor = 3.5f;
    public float driftFactor = 0.95f;
    public float maxSpeed = 20;

    float velocityVsUp = 0;

    float accelerationInput = 0;
    float steeringInput = 0;

    float rotationAngle = 0;

    Rigidbody2D carRigidBody2D;

    //called when the script instance is loaded
     void Awake()
    {
        carRigidBody2D = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
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
        //limit carss ability to turn whrn moving slowly
        float minSpeedBeforeAllowTurnFactor = (carRigidBody2D.velocity.magnitude / 8);
        minSpeedBeforeAllowTurnFactor = Mathf.Clamp01(minSpeedBeforeAllowTurnFactor);
        rotationAngle -= steeringInput * turnFactor * minSpeedBeforeAllowTurnFactor;
        carRigidBody2D.MoveRotation(rotationAngle);
    }

    void KillOrthogonalVelocity()
    {
        //make the car physics mimic a real car with adding car rtire friction
        Vector2 forwardVelocity = transform.up * Vector2.Dot(carRigidBody2D.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(carRigidBody2D.velocity, transform.right);

        carRigidBody2D.velocity = forwardVelocity + rightVelocity * driftFactor;
    }
    float GetLateralVelocity()
    {
        return Vector2.Dot(transform.right, carRigidBody2D.velocity);
    }

    public bool IsTireScreeching(out float laterVelocity, out bool isBraking)
    {
        laterVelocity = GetLateralVelocity();
        isBraking = false;

        //check if we are moving forward and player hitting brakes, 
        if (accelerationInput < 0 && velocityVsUp > 0)
        {
            isBraking = true;
            return true;
        }
        //a lot of side movement
        if (Mathf.Abs(GetLateralVelocity()) > 1.0f)
            return true;

        return false;
        
    }

    public void SetInputVector(Vector2 inputVector)
    {
        steeringInput = inputVector.x;
        accelerationInput = inputVector.y;
    }
}
