using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitFinishLine : MonoBehaviour
{
    public AudioSource winAudio;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        winAudio.Play();
    }
}
