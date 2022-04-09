using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonBall : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        var robotPlayer = collision.collider.GetComponent<RobotTouchController>();

        if (robotPlayer)
        {
            Destroy(robotPlayer.gameObject);
            Destroy(this.gameObject);
        }
    }
}
