using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour, IInteract
{
    [SerializeField] private string prompt;
    [SerializeField] private ItemData itemName;
    [SerializeField] private Iventory iventoryUpdate;


    private void Update()
    {
        prompt = itemName.name;
    }
    public string InteractionPrompt { get => prompt; }
    public bool Interact(InteractSystem interactor)
    {
        Debug.Log("Pick Up " + prompt);
        bool wasPickUp = iventoryUpdate.AddItem(itemName);
        if (wasPickUp)
        {
            Destroy(gameObject);
        }
        return true;
    }
}
