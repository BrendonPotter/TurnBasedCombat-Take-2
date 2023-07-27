using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageStory : MonoBehaviour
{
    // The target object to move towards
    public Transform targetObject;

    // The speed at which the object moves towards the target
    public float moveSpeed = 5f;

    // The rotation speed to rotate the object towards the target
    public float rotationSpeed = 5f;

    // Whether the object is currently moving towards the target
    private bool isMoving = false;

    public MonoBehaviour targetScript;

    [SerializeField] GameObject magePanel;

    [SerializeField] GameObject mageModel;

    [SerializeField] SaveSystem hunterStats;

    // Update is called once per frame
    private void Update()
    {
        // Check if the object is currently moving and hasn't reached its destination
        if (isMoving)
        {
            // Move and rotate the object towards the target
            MoveAndRotateTowardsTarget();
        }

        if(isMoving == false)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                magePanel.SetActive(false);
                targetScript.enabled = true;
                mageModel.SetActive(false);
                hunterStats.checkPoint = 1;
            }
        }
    }

    // Called when the object enters a trigger collider
    private void OnTriggerEnter(Collider other)
    {
        // Check if the trigger's tag matches the target object's tag
        if (other.CompareTag(targetObject.tag))
        {
            if (targetScript != null)
            {
                targetScript.enabled = false;
            }

            // Start moving towards the target object
            isMoving = true;
        }
    }

    // Move and rotate the object towards the target
    private void MoveAndRotateTowardsTarget()
    {
        // Calculate the direction towards the target (only consider x and z axes)
        Vector3 direction = new Vector3(targetObject.position.x - transform.position.x, 0f, targetObject.position.z - transform.position.z).normalized;

        // Calculate the rotation to look at the target
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        //// Smoothly rotate the object towards the target
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Move the object towards the target (only along x and z axes)
        transform.position += direction * moveSpeed * Time.deltaTime;

        // Check if the object has reached the target
        float distanceToTarget = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(targetObject.position.x, targetObject.position.z));
        float stoppingDistance = 2f;
        if (distanceToTarget <= stoppingDistance)
        {
            // Mark the object as having reached its destination
            isMoving = false;
          
            magePanel.SetActive(true);
            Debug.Log("we working");
        }
    }
}
