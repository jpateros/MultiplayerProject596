using UnityEngine;
/// <summary>
/// This class controls the logic for adjusting the SFX output based on game states.
/// </summary>
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

    //3 primary sources of sound: Engine, Screeching/Braking, Collisions
    void Update()
    {
        UpdateEngineSFX();
        UpdateScreechSFX();
    }

    //The Screech sound volume is updated based on braking conditions and later velocity
    private void UpdateScreechSFX()
    {
        if (carController.IsTireScreeching(out float lateralVelocity, out bool isBraking))
        {
            //If car us brakin want tire screech louder with small pitch adjustment
            if (isBraking)
            {
                tiresScreechAudio.volume = Mathf.Lerp(tiresScreechAudio.volume, 1.0f, Time.deltaTime * 10);
                tiresScreechPitch = Mathf.Lerp(tiresScreechPitch, 0.5f, Time.deltaTime * 10);
            }
            else
            {
                //not braking should should still have screech sobd id we drift based on magnitude of later velocity
                tiresScreechAudio.volume = Mathf.Abs(lateralVelocity) * 0.05f;
                tiresScreechPitch = Mathf.Abs(lateralVelocity) * 0.1f;
            }
        }
        //fade the tire screech SFX sound if we are not actually screeching
        else
        {
            tiresScreechAudio.volume = Mathf.Lerp(tiresScreechAudio.volume, 0, Time.deltaTime * 10);
        }
    }

    //Method updates the engine SFX based on car speed 
    private void UpdateEngineSFX()
    {
        float velocityMagnitude = carController.GetVelocityMagnitude();
        float desiredEngineVolume = velocityMagnitude * 0.05f;

        //Decrease the car volume when idle
        desiredEngineVolume = Mathf.Clamp(desiredEngineVolume, 0.2f, 1.0f);
        engineAudio.volume = Mathf.Lerp(engineAudio.volume, desiredEngineVolume, Time.deltaTime * 10);

        //Add variation to the engine sound by playing with the pitch
        desiredEnginePitch = velocityMagnitude * 0.2f;
        desiredEnginePitch = Mathf.Clamp(desiredEnginePitch, 0.5f, 1.0f);
        engineAudio.pitch = Mathf.Lerp(engineAudio.pitch, desiredEnginePitch, Time.deltaTime * 1.5f);
    }

    //Method handles the SFX sounds based on the collusuon 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //The magnitude of our volume will depends on hard hard the collision was
        float relativeVel = collision.relativeVelocity.magnitude;
        float volume = relativeVel * 0.1f;
        //Random pitch range is used to keep it interesting
        carHitAudio.pitch = UnityEngine.Random.Range(0.95f, 1.05f);
        carHitAudio.volume = volume;

        //Play standard crash audio as long as we are not currently playing it and we have not hit finish
        if (!carHitAudio.isPlaying && collision.gameObject.tag != "Finish") carHitAudio.Play();
        //Otherwise play the winner audii when we hit the finish line
        else if (!winner.isPlaying && collision.gameObject.tag == "Finish") winner.Play();
       
    }
}