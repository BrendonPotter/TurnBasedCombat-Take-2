using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowThePlayer : MonoBehaviour
{
    public NavMeshAgent agent;
    [SerializeField] private AINavMesh moveBack;

    public Transform player;
    [SerializeField] float distanceFromPlayer;
    //See if the player is in range
    public bool playerIsInRange;
    public bool isStopped;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (playerIsInRange)
        {
            ChasePlayer();
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= distanceFromPlayer)
        {
            isStopped = true;
            agent.isStopped = true;
        }
        else
        {
            isStopped = false;
            agent.isStopped = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsInRange = false;
            moveBack.UpdateDestination();
        }
    }

    private void ChasePlayer()
    {
        if (isStopped == false)
        {
            transform.LookAt(player);
            agent.SetDestination(player.position);
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        }
    }
}
