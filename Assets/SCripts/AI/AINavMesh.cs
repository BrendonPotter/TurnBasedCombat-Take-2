using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AINavMesh : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    public FollowThePlayer followPlayerScript;
    public Transform[] waypoints; //array of waypoint for the AI to go to.
    int waypointIndex;

    Vector3 target;
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        UpdateDestination();
        followPlayerScript.enabled = true;
    }

    private void Update()
    {
        StartMoving();
    }

    private void StartMoving()
    {
        if (Vector3.Distance(transform.position, target) < 1)
        {
            WayPointIndex();
            UpdateDestination();
        }
    }

    public void UpdateDestination()
    {
        target = waypoints[waypointIndex].position;
        navMeshAgent.SetDestination(target);
    }

    void WayPointIndex()
    {
        waypointIndex++;
        if(waypointIndex == waypoints.Length)
        {
            waypointIndex = 0; //if the current waypoint on equal to the assigned amount of waypoint - reset back to zero
        }
    }
}
