using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour, IInteract
{
    [SerializeField] private string prompt;
    [SerializeField] private Door grabKey;

    public string InteractionPrompt { get => prompt; }
    public bool Interact(InteractSystem interactor)
    {
        grabKey.haveKey= true;
        Debug.Log("Obtain key!");
        Destroy(this.gameObject);
        return true;
    }
}
