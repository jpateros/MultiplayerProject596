using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class updates the visual efects of the particle trailer behind the car.
/// </summary>
public class WheelParticleHandler : MonoBehaviour
{
    float particleEmissionRate = 0;
    CarController carController;
    ParticleSystem particleSystemSmoke;
    ParticleSystem.EmissionModule particleSystemEM;

    private void Awake()
    {
        carController = GetComponentInParent<CarController>();
        particleSystemSmoke = GetComponent<ParticleSystem>();
        particleSystemEM = particleSystemSmoke.emission;
        particleSystemEM.rateOverTime = 0;
    }
   
    //Update the particles from behind the wheel as a function of car mechanics
    void Update()
    {
        //Reuces the total amount of particles over time
        particleEmissionRate = Mathf.Lerp(particleEmissionRate, 0, Time.deltaTime * 5);
        particleSystemEM.rateOverTime = particleEmissionRate;

        //If the car is screeching and 
        if (carController.IsTireScreeching(out float lateralVelocitty, out bool isBraking))
        {
            if (isBraking) particleEmissionRate = 30;
            //emit based on how much the plyaer is drifting
            else particleEmissionRate = Mathf.Abs(lateralVelocitty) * 2;
        }

    }
}