using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class NpcBehaviour : MonoBehaviour, IInteract
{
    public Transform targetObject;

    [SerializeField] GameObject disableObjects;
    [SerializeField] GameObject askingForHelp;
    [SerializeField] GameObject agreeToHelp;


    [SerializeField] MoveToWayPoint stopMovement;
    [SerializeField] bool isLooking;
    [SerializeField] float delayAction;

    [SerializeField] private string prompt;

    public string InteractionPrompt { get => prompt; }
    public bool Interact(InteractSystem interactor)
    {
        agreeToHelp.SetActive(true);
        askingForHelp.SetActive(false);
        Debug.Log("Talk to the child");
        return true;
    }

    private void Update()
    {
        if(isLooking == true)
        {
            delayAction -= Time.deltaTime;
            if(delayAction <= 0)
            {
                UpdateLookAtRotation();
                disableObjects.SetActive(false);
                askingForHelp.SetActive(true);
            }
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
        delayAction = 0.25f;

        disableObjects.SetActive(true);
        askingForHelp.SetActive(false);
        agreeToHelp.SetActive(true);

    }

    public void UpdateLookAtRotation()
    {
        Vector3 lookAtDirection = targetObject.position - transform.position;
        lookAtDirection.y = 0f; // Optional: Lock rotation on the y-axis
        Quaternion rotation = Quaternion.LookRotation(lookAtDirection);
        transform.rotation = rotation;
    }
}
