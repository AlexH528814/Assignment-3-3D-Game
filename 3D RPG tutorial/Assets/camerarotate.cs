using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerarotate : MonoBehaviour
{
    // The two points that the camera should rotate between
    public Transform pointA;
    public Transform pointB;

    // The speed at which the camera should rotate
    public float rotationSpeed;

    // The current destination for the camera
    public Transform destination;

    void Start()
    {
        // Set the initial destination for the camera
        destination = pointB;
    }

    void Update()
    {
        // Rotate the camera towards the destination
        transform.rotation = Quaternion.Lerp(transform.rotation, destination.rotation, rotationSpeed * Time.deltaTime);

        // If the camera has reached the destination, switch to the other point
        if (transform.rotation == destination.rotation && destination == pointB)
        {
            destination = pointA;
        }

        else if (transform.rotation == destination.rotation && destination == pointA)
        {
            destination = pointB;
        }
    }
}
