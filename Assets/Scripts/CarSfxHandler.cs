using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSfxHandler : MonoBehaviour
{
    [Header("Audio sources")]
    public AudioSource tiresScreechAudio;
    public AudioSource engineAudio;
    public AudioSource carHitAudio;
    public AudioSource winner;
    float desiredEnginePitch = 0.5f;
    float tiresScreechPitch = 0.5f;

    CarController carController;
    private void Awake()
    {
        carController = GetComponentInParent<CarController>();

    }
        // Start is called before the first frame update
        void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEngineSFX();
        UpdateScreechSFX();
    }

    private void UpdateScreechSFX()
    {
        if (carController.IsTireScreeching(out float lateralVelocity, out bool isBraking))
            if (isBraking)
            {
                tiresScreechAudio.volume = Mathf.Lerp(tiresScreechAudio.volume, 1.0f, Time.deltaTime * 10);
                tiresScreechPitch = Mathf.Lerp(tiresScreechPitch, 0.5f, Time.deltaTime * 10);
            }
        else
            {
                tiresScreechAudio.volume = Mathf.Abs(lateralVelocity) * 0.05f;
                tiresScreechPitch = Mathf.Abs(lateralVelocity) * 0.1f;
            }
        else
        {
            tiresScreechAudio.volume = Mathf.Lerp(tiresScreechAudio.volume, 0, Time.deltaTime * 10);
        }
    }

    private void UpdateEngineSFX()
    {
        float velocityMagnitude = carController.GetVelocityMagnitude();

        float desiredEngineVolume = velocityMagnitude * 0.05f;

        desiredEngineVolume = Mathf.Clamp(desiredEngineVolume, 0.2f, 1.0f);

        engineAudio.volume = Mathf.Lerp(engineAudio.volume, desiredEngineVolume, Time.deltaTime * 10);

        desiredEnginePitch = velocityMagnitude * 0.2f;
        desiredEnginePitch= Mathf.Clamp(desiredEnginePitch, 0.5f, 1.0f);
        engineAudio.pitch = Mathf.Lerp(engineAudio.pitch, desiredEnginePitch, Time.deltaTime * 1.5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float relativeVel = collision.relativeVelocity.magnitude;

        float volume = relativeVel * 0.1f;

        carHitAudio.pitch = UnityEngine.Random.Range(0.95f, 1.05f);
        carHitAudio.volume = volume;

        if (!carHitAudio.isPlaying && collision.gameObject.tag != "Finish")
            carHitAudio.Play();
        else if (!winner.isPlaying && collision.gameObject.tag == "Finish")
        {
            winner.Play();
        }
    }
}
