using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public AudioSource laserAudioSource;
    public AudioClip explosionSound;

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Planet")
        {
            laserAudioSource.PlayOneShot(explosionSound);
        }
        
    }
}
