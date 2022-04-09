using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonTower : MonoBehaviour
{
    [SerializeField]
    private Rigidbody cannonBallPrefab;
    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    private float shootingForce;
    [SerializeField]
    private float turnSpeed;
    [SerializeField]
    private float maxCountdown = 5f;

    private float shootCountdown = 5f;

    
    void Update()
    {
        // check to see if the robot player is in the scene
        if (!RobotPlayer())
        {
            return;
        }

        // rotate towards the robot player
        Vector3 targetDirection = RobotPlayer().transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(targetDirection);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(0, rotation.y, 0);

        // shoot at player
        shootCountdown -= Time.deltaTime;
        if(shootCountdown <= 0)
        {
            ShootAtPlayer();
            shootCountdown = maxCountdown;
        }
    }

    /// <summary>
    /// Shoot the cannonball
    /// </summary>
    private void ShootAtPlayer()
    {
        Rigidbody cannonBall = Instantiate(cannonBallPrefab, spawnPoint.position, spawnPoint.rotation);
        cannonBall.AddForce(cannonBall.transform.forward * shootingForce);
        Destroy(cannonBall.gameObject, 2f);
    }

    /// <summary>
    /// Returns the robot player
    /// </summary>
    /// <returns></returns>
    private GameObject RobotPlayer()
    {
        RobotTouchController robotPlayer = FindObjectOfType<RobotTouchController>();

        if (robotPlayer)
        {
            return robotPlayer.gameObject;
        }

        return null;
    }
}
