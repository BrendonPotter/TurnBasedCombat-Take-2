using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class TurtorialUI : MonoBehaviour, IInteract
{
    [SerializeField] private string prompt;

    [SerializeField] private GameObject closeTurtorial;
    [SerializeField] GameObject closeTurtorialUI;

    public string InteractionPrompt { get => prompt; }
    public bool Interact(InteractSystem interactor)
    {
        closeTurtorial.SetActive(false);
        closeTurtorialUI.SetActive(false);
        Debug.Log("Close UI!");
        return true;
    }
}
