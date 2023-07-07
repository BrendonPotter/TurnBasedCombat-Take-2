using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSafeguard : MonoBehaviour
{
    public Vector3 teleportPosition = new Vector3(0f, 1f, 0f);
    public float fallingDepth;

    private void Start()
    {
        transform.position = teleportPosition;
    }

    private void Update()
    {
        if (transform.position.y < fallingDepth)
        {
            transform.position = teleportPosition;
        }
    }
}
