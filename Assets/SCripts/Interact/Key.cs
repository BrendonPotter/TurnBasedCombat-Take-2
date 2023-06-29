using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour, IInteract
{
    [SerializeField] private string prompt;
    [SerializeField] private WorldState grabKey;

    public string InteractionPrompt { get => prompt; }
    public bool Interact(InteractSystem interactor)
    {
        grabKey.obtainKey= true;
        Debug.Log("Obtain key!");
        Destroy(this.gameObject);
        return true;
    }

    private void Update()
    {
        if (grabKey.obtainKey == true)
        {
            Destroy(this.gameObject);
        }
    }
}
