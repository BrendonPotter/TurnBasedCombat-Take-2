using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToWayPoint : MonoBehaviour
{
    public Transform[] points;
    public float movementSpeed = 5f;
    public float pauseDuration = 1f;

    private int currentPointIndex = 0;
    private Vector3 startingPosition;
    private bool isMoving = false;

    private void Start()
    {
        startingPosition = transform.position;
        MoveToNextPoint();
    }

    private void Update()
    {
        if (isMoving)
        {
            MoveToPoint(points[currentPointIndex].position);

            Vector3 lookAtDirection = points[currentPointIndex].position - transform.position;
            lookAtDirection.y = 0f; // Optional: Lock rotation on the y-axis
            Quaternion rotation = Quaternion.LookRotation(lookAtDirection);
            transform.rotation = rotation;
        }
    }

    private void MoveToPoint(Vector3 targetPosition)
    {
        float step = movementSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        if (transform.position == targetPosition)
        {
            StartCoroutine(PauseBeforeNextMove());
        }
    }

    private IEnumerator PauseBeforeNextMove()
    {
        isMoving = false;
        yield return new WaitForSeconds(pauseDuration);
        MoveToNextPoint();
    }

    private void MoveToNextPoint()
    {
        currentPointIndex++;
        if (currentPointIndex >= points.Length)
        {
            currentPointIndex = 0;
            StartCoroutine(ReturnToStartingPosition());
        }
        else
        {
            isMoving = true;
        }
    }

    private IEnumerator ReturnToStartingPosition()
    {
        yield return new WaitForSeconds(pauseDuration);
        isMoving = true;
        MoveToPoint(startingPosition);
    }
}
