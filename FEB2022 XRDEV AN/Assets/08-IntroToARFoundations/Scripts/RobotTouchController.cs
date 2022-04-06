using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotTouchController : MonoBehaviour
{
    public float moveSpeed = 30f;
    public float turnSpeed = 5f;
    
    private Joystick joystick;
    private Rigidbody robotRigidbody;
    private Animator robotAnim;
    
    void OnEnable()
    {
        joystick = FindObjectOfType<Joystick>();
        robotRigidbody = GetComponent<Rigidbody>();
        robotAnim = GetComponent<Animator>();

        robotAnim.SetBool("Open_Anim", true);
    }

    
    void Update()
    {
        // rotation
        Vector3 targetDirection = new Vector3(joystick.Direction.x, 0, joystick.Direction.y);
        Vector3 rotatingVector = Vector3.RotateTowards(transform.forward, targetDirection, Time.deltaTime * turnSpeed, 0.0f);
        transform.rotation = Quaternion.LookRotation(rotatingVector);

        // movement
        if(joystick.Direction.magnitude > 0)
        {
            // move robot forward
            robotRigidbody.AddForce(transform.forward * moveSpeed);

            // set robot walking animation
            robotAnim.SetBool("Walk_Anim", true);
        }
        else
        {
            // set idle robot animation
            robotAnim.SetBool("Walk_Anim", false);
        }
    }
}
