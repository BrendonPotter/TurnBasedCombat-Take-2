using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteract
{
    [SerializeField] private string prompt;
    [SerializeField] private WorldState grabKey;

    private bool alreadyInteract;
    public string InteractionPrompt { get => prompt; }
    public bool Interact(InteractSystem interactor)
    {
        if(grabKey.obtainKey == true)
        {
            alreadyInteract = true;
            Debug.Log("Opening door!");
            Destroy(this.gameObject);
        }
        else
        {
            Debug.Log("Gate Locked");
        }
        return true;
    }

    private void Update()
    {
        if (grabKey.obtainKey == true && alreadyInteract == true)
        {
            Destroy(this.gameObject);
        }
    }
}
