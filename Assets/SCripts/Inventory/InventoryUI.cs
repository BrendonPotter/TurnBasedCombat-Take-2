using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    Iventory inventory;
    // Start is called before the first frame update
    void Start()
    {
        inventory = Iventory.instance;
        inventory.onItemChangeCallBack += UpdateUI;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateUI()
    {
        Debug.Log("Updating UI");
    }
}
