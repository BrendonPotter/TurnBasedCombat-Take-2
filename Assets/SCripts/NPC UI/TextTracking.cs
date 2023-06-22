using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTracking : MonoBehaviour
{
    public Transform player;
    private void Update()
    {
        transform.rotation = player.rotation;
    }
}
