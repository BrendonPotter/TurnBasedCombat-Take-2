using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSafeguard : MonoBehaviour
{
    private Vector3 teleportPosition = new Vector3(0f, 1f, 0f);

    private void FixedUpdate()
    {
        if (transform.position.y < -300f)
        {
            transform.position = teleportPosition;
        }
    }
}
