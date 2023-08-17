using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Flee : MonoBehaviour
{
    public Transform targetObject;
    public float avoidanceDistance = 3f;

    private NavMeshAgent navMeshAgent;

    [SerializeField] GameObject ohSnapCanvas;
    [SerializeField] GameObject isRunTimeCanvas;


    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        Fleeing();
    }

    public void Fleeing()
    {
        if (targetObject != null)
        {
            // Calculate the distance between this object and the target object
            float distanceToTarget = Vector3.Distance(transform.position, targetObject.position);

            // Check if the distance is less than the avoidance distance
            if (distanceToTarget < avoidanceDistance)
            {
                ohSnapCanvas.SetActive(true);
                isRunTimeCanvas.SetActive(false);

                // Calculate a vector away from the target object
                Vector3 avoidanceDirection = transform.position - targetObject.position;

                // Normalize the direction vector and apply it to the NavMeshAgent's destination
                Vector3 newDestination = transform.position + avoidanceDirection.normalized * avoidanceDistance;
                navMeshAgent.SetDestination(newDestination);
            }
        }
    }
}
