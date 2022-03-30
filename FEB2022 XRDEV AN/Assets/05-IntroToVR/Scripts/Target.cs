using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Target : MonoBehaviour
{
    public float moveSpeed;
    public float moveAmount;
    public FoodFightGame game;
    public int points;
    private float startingXPosition;

    
    void Awake()
    {
        startingXPosition = transform.position.x;
    }

    
    void Update()
    {
        var newPosition = transform.position;
        newPosition.x = startingXPosition + Mathf.Sin(Time.time * moveSpeed) * moveAmount;
        transform.position = newPosition;
    }

    private void OnCollisionEnter(Collision collision)
    {
        var foodStuff = collision.gameObject.GetComponent<XRGrabInteractable>();

        if(foodStuff != null)
        {
            game.OnTargetHit(this);

            Destroy(this.gameObject);
        }
    }
}
