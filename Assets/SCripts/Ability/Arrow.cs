using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 10f;
    public float travelTime = 1f;

    private Vector3 targetPosition;
    private float elapsedTime = 0f;

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
