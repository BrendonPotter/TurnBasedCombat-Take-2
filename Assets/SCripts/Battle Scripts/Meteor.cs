using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public float speed = 10f;
    public static float travelTime = 2f;
    private float elapsedTime = 0f;

    private Vector3 targetPosition;

    public void SetTarget(Vector3 target)
    {
        targetPosition = target;
    }

    private void Update()
    {
        // Move towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Check if the arrow has reached the target position
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= travelTime)
        {
            Destroy(gameObject);
        }
    }
}
