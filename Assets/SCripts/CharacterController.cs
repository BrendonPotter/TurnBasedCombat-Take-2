using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;
    public float lookSensitivity = 2f;
    public float maximumLookUpAngle = 90f;
    public float maximumLookDownAngle = 90f;

    private Rigidbody rb;
    private bool isJumping = false;
    private float rotationX = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Lock cursor to the game window
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        // Movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical) * speed * Time.deltaTime;
        rb.MovePosition(transform.position + transform.TransformDirection(movement));

        // Jumping
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = true;
        }

        // Mouse look
        float mouseX = Input.GetAxis("Mouse X") * lookSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -maximumLookDownAngle, maximumLookUpAngle);

        transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        rb.transform.Rotate(Vector3.up * mouseX);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the character is touching the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }
}
