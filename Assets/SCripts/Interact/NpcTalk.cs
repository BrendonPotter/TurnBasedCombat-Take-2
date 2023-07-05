using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcTalk : MonoBehaviour, IInteract
{
    [SerializeField] private string prompt;

    [SerializeField] List<GameObject> objects;
    [SerializeField] GameObject finalObject;

    [SerializeField] int currentIndex = 0;
    public bool scriptEnabled = true;

    public string InteractionPrompt { get => prompt; }

    public bool Interact(InteractSystem interactor)
    {
        if (Input.GetKeyDown(KeyCode.E) && scriptEnabled)
        {
            objects[currentIndex].SetActive(false);
            currentIndex++;

            if (currentIndex == objects.Count)
            {
                scriptEnabled = false;
                finalObject.SetActive(true);
            }

            objects[currentIndex].SetActive(true);
        }
        return true;
    }
}
