using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteract
{
    [SerializeField] private string prompt;
    public bool haveKey;

    public string InteractionPrompt { get => prompt; }
    public bool Interact(InteractSystem interactor)
    {
        if(haveKey == true)
        {
            Debug.Log("Opening door!");
            Destroy(this.gameObject);
        }
        else
        {
            Debug.Log("Gate Locked");
        }
        return true;
    }
}
