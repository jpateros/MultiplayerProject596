using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //should only emit is the tires are screeching 
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
