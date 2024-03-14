using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LMCamMovement : MonoBehaviour
{
    [SerializeField] GameObject destination;

    // Adjust this value to control the speed of movement
    [SerializeField] float movementSpeed = 1.0f;

    void Update()
    {
        // Move the camera towards the destination position gradually
        transform.position = Vector3.Lerp(transform.position, destination.transform.position, movementSpeed * Time.deltaTime);
    }

}
