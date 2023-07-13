using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class NpcBehaviour : MonoBehaviour, IInteract
{
    public Transform targetObject;

    [SerializeField] GameObject askingForHelp;
    [SerializeField] GameObject agreeToHelp;


    [SerializeField] MoveToWayPoint stopMovement;
    [SerializeField] WorldState worldState;
    [SerializeField] bool isLooking;

    [SerializeField] private string prompt;

    public string InteractionPrompt { get => prompt; }
    public bool Interact(InteractSystem interactor)
    {
        worldState.agreeToPlay = true;
        Debug.Log("Talk to the child");
        return true;
    }

    private void Update()
    {
        if(isLooking == true)
        {
            UpdateLookAtRotation();
        }

        if(worldState.agreeToPlay == true)
        {
            stopMovement.enabled = false;
            agreeToHelp.SetActive(true);
            askingForHelp.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            isLooking = true;
            stopMovement.enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        stopMovement.enabled = true;
        isLooking = false;

    }

    public void UpdateLookAtRotation()
    {
        Vector3 lookAtDirection = targetObject.position - transform.position;
        lookAtDirection.y = 0f; // Optional: Lock rotation on the y-axis
        Quaternion rotation = Quaternion.LookRotation(lookAtDirection);
        transform.rotation = rotation;
    }
}
