using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer : MonoBehaviour
{
    public Transform target;
    public Animator animator;
    NavMeshAgent nav;

    private bool isWalking = false;
    private bool playerIsMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        nav.updateRotation = false; // Disable NavMeshAgent rotation
    }

    // Update is called once per frame
    void Update()
    {
        if (nav.remainingDistance > nav.stoppingDistance)
        {
            if (!isWalking)
            {
                animator.Play("Walk_N");
                isWalking = true;
            }
        }
        else
        {
            if (isWalking)
            {
                animator.Play("Idle");
                isWalking = false;
            }
        }

        if (Vector3.Distance(target.position, transform.position) > 0.1f)
        {
            playerIsMoving = true;
            nav.SetDestination(target.position);
        }
        else
        {
            playerIsMoving = false;
        }

        if (!nav.pathPending && !playerIsMoving)
        {
            Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
    }
}
