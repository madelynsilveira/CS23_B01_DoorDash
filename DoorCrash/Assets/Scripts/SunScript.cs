using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunScript : MonoBehaviour
{
    private Rigidbody rb;
    public float initialSpeed = 5f; 
    public float arcHeight = 2f;    

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Launch();
    }

    private void Launch()
    {
        // Calculate the initial velocity for the desired arc.
        Vector3 launchVelocity = CalculateLaunchVelocity();

        // Apply the velocity to the Rigidbody to make the ball move.
        rb.velocity = launchVelocity;
    }

    private Vector3 CalculateLaunchVelocity()
    {
        // Calculate the required horizontal velocity.
        float horizontalVelocity = initialSpeed;

        // Calculate the required vertical velocity for the desired arc.
        float verticalVelocity = Mathf.Sqrt(2 * arcHeight * Mathf.Abs(Physics.gravity.y));

        // Create the launch velocity vector.
        Vector3 launchVelocity = transform.forward * horizontalVelocity + transform.up * verticalVelocity;

        return launchVelocity;
    }
}


