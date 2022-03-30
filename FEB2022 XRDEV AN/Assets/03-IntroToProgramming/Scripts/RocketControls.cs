using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketControls : MonoBehaviour
{
    public float turningThrust;
    public float thrust;
    public float drag;
    public Rigidbody laserPrefab;
    public Transform spawnPoint;
    public float laserImpulse;
    public AudioClip laserSound;

    private Rigidbody rocketRigidbody;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rocketRigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = -Input.GetAxis("Mouse Y");

            rocketRigidbody.AddRelativeTorque(0, mouseX * turningThrust * Time.deltaTime, 0);
            rocketRigidbody.AddRelativeTorque(mouseY * turningThrust * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey(KeyCode.W))
        {
            // move forward
            rocketRigidbody.AddForce(transform.forward * Time.deltaTime * thrust);
        }
        if (Input.GetKey(KeyCode.S))
        {
            // move backwards
            rocketRigidbody.AddForce(transform.forward * Time.deltaTime * -thrust);
        }

        // Challenge 1
        if (Input.GetKey(KeyCode.A))
        {
            // move left
            rocketRigidbody.AddForce(transform.right * Time.deltaTime * -thrust);
        }
        if (Input.GetKey(KeyCode.D))
        {
            // move right
            rocketRigidbody.AddForce(transform.right * Time.deltaTime * thrust);
        }

        // Challenge 2
        rocketRigidbody.AddForce(-rocketRigidbody.velocity * Time.deltaTime * drag);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireLaser();
        }

    }

    private void FireLaser()
    {
        // create a laser in the scene at a position and rotation
        Rigidbody laser = Instantiate(laserPrefab, spawnPoint.position, spawnPoint.rotation);

        // match the laser velocity with the rocket velocity
        laser.velocity = rocketRigidbody.velocity;

        // give the laser an impulse to shoot it forward
        laser.AddForce(transform.forward * laserImpulse);

        // play laser sound
        audioSource.PlayOneShot(laserSound);

        Destroy(laser, 5f);
    }
}

