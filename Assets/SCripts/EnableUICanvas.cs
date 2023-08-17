using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableUICanvas : MonoBehaviour
{
    [SerializeField] GameObject enableUICanvas;

    public float countDown;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("EnableCanvas", countDown);
    }

    // Update is called once per frame
    void EnableCanvas()
    {
        enableUICanvas.SetActive(true);
    }
}
