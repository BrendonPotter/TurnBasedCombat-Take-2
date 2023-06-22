using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAndReset : MonoBehaviour
{
    public Transform targetPosition;  // The position to move towards
    public float moveTime = 2f;       // The time it takes to move
    public float resetTime = 5f;      // The time to reset position

    private float timer = 0f;         // Timer to keep track of time
    private Vector3 initialPosition;  // The initial position of the object

    private void Start()
    {
        // Store the initial position of the object
        initialPosition = transform.position;
    }

    private void Update()
    {
        // Update the timer
        timer += Time.deltaTime;

        // If the timer exceeds the reset time, reset the position of the object
        if (timer > resetTime)
        {
            transform.position = initialPosition;
            timer = 0f;
        }
        else
        {
            // Otherwise, move the object towards the target position using Lerp
            transform.position = Vector3.Lerp(initialPosition, targetPosition.position, timer / moveTime);
        }
    }
}
