using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Rigidbody))]
public class Gun : XRGrabInteractable
{
    public Rigidbody projectilePrefab;
    public Transform spawnPoint;
    public float shootingImpulse;
    public AudioClip shootingSound;

    private AudioSource audioSource;

    
    void Start()
    {
        activated.AddListener(FireProjectile);
        audioSource = GetComponent<AudioSource>();
    }

    private void FireProjectile(ActivateEventArgs args)
    {
        // create a projectile in the scene at a position and rotation
        Rigidbody laser = Instantiate(projectilePrefab, spawnPoint.position, spawnPoint.rotation);

        // give the projectile an impulse to shoot it forward
        laser.AddForce(transform.forward * shootingImpulse);

        // play projectile sound
        audioSource.PlayOneShot(shootingSound);

        Destroy(laser, 5f);
    }
}
