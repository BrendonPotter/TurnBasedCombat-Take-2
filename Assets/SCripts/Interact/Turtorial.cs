using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtorial : MonoBehaviour, IInteract
{
    [SerializeField] private string prompt;
    [SerializeField] GameObject turtorialCanvas;
    [SerializeField] GameObject turtorialCollider;


    public string InteractionPrompt { get => prompt; }
    public bool Interact(InteractSystem interactor)
    {
        Destroy(turtorialCanvas);
        Destroy(turtorialCollider);
        Debug.Log("Close tutorial!");
        return true;
    }
}
