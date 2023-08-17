using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageStory : MonoBehaviour
{
    // The speed at which the object moves towards the target
    public float moveSpeed = 5f;
    public float rotationSpeed = 5f;

    private bool isMoving = false;
    private bool hasTriggered = false;

    [SerializeField] GameObject magePanel;
    [SerializeField] GameObject mageModel;
    [SerializeField] SaveSystem hunterStats;

    private GameObject playerCharacter; // Reference to the player character

    private void Start()
    {
        // Find the player character dynamically based on its tag
        playerCharacter = GameObject.FindGameObjectWithTag("Player");

        if (playerCharacter != null)
        {
            isMoving = false; // Make sure it isnt moving towards the player character
        }
        else
        {
            Debug.LogError("Player character not found with tag 'Player'");
        }
    }

    private void Update()
    {
        if (isMoving == true)
        {
            MoveAndRotateTowardsTarget();
        }

        if (isMoving == false && hasTriggered == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                magePanel.SetActive(false);
                mageModel.SetActive(false);
                hunterStats.checkPoint = 1;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isMoving = true;
        }
    }

    private void MoveAndRotateTowardsTarget()
    {

        Vector3 direction = (playerCharacter.transform.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        Vector3 newPosition = transform.position + new Vector3(direction.x, 0f, direction.z) * moveSpeed * Time.deltaTime;
        newPosition.y = transform.position.y;

        transform.position = newPosition;

        float distanceToTarget = Vector3.Distance(new Vector3(transform.position.x, 0f, transform.position.z), new Vector3(playerCharacter.transform.position.x, 0f, playerCharacter.transform.position.z));
        float stoppingDistance = 2f;
        if (distanceToTarget <= stoppingDistance)
        {
            isMoving = false;
            hasTriggered = true;

            magePanel.SetActive(true);
            Debug.Log("we working");
        }
    }
}
