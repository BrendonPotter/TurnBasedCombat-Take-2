using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpUI : MonoBehaviour
{
    [SerializeField] WorldState state;

    [SerializeField] GameObject canvas;
    [SerializeField] GameObject initialCanvas;


    private void Update()
    {
        if(state.contactEnemy2 == true)
        {
            canvas.SetActive(true);
            initialCanvas.SetActive(false);
        }
    }
}
