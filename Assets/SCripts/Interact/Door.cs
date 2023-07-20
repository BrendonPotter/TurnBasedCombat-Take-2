using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteract
{
    [SerializeField] private string prompt;
    [SerializeField] private WorldState grabKey;
    [SerializeField] GameObject gateLockTXT;

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
            gateLockTXT.SetActive(true);
            StartCoroutine(EnableDisableRoutine());
        }
        return true;
    }

    private IEnumerator EnableDisableRoutine()
    {
        // Wait for 2 seconds
        yield return new WaitForSeconds(2f);

        // Disable the object after waiting
        gateLockTXT.SetActive(false);
    }

    private void Update()
    {
        if (grabKey.obtainKey == true && alreadyInteract == true)
        {
            Destroy(this.gameObject);
        }
    }
}
