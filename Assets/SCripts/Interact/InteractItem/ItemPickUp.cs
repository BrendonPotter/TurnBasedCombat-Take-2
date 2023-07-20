using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour, IInteract
{
    [SerializeField] private string prompt;
    [SerializeField] private Inventory itemPickup;
    [SerializeField] Item itemScript;

    private void Update()
    {

    }
    public string InteractionPrompt { get => prompt; }
    public bool Interact(InteractSystem interactor)
    {
        Debug.Log("Pick Up " + prompt);
        var item = GetComponent<Item>();
        if (item)
        {
            itemPickup.AddItem(item.itemObjectScript, 1);
        }
        Destroy(gameObject);
        return true;
    }
}
