using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //reduce paticles over time
        particleEmissionRate = Mathf.Lerp(particleEmissionRate, 0, Time.deltaTime * 5);
        particleSystemEM.rateOverTime = particleEmissionRate;

        if (carController.IsTireScreeching(out float lateralVelocitty, out bool isBraking))
        {
            if (isBraking)
                particleEmissionRate = 30;
            //emit based on how much the plyaer is drifting
            else particleEmissionRate = Mathf.Abs(lateralVelocitty) * 2;
        }



    }
}
