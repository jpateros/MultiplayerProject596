using UnityEngine;

/// <summary>
/// This class will decide how much of the trailing smoke to render based on how much our tires screech
/// </summary>
public class WheelTrailRendererHandler : MonoBehaviour
{
    CarController carController;
    TrailRenderer trailRender;

    private void Awake()
    {
        carController = GetComponentInParent<CarController>();
        trailRender = GetComponent<TrailRenderer>();
        trailRender.emitting = false;
    }
    
    
    void Update()
    {
        //Shoukd only emit if the tires are screeching 
        if (carController.IsTireScreeching(out float laterVelocity, out bool isBraking))
        {
            trailRender.emitting = true;
        }
        else
        {
            trailRender.emitting = false;
        }

    }
}
