using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject inventoryUI;
    InventorySlot[] slots;

    [SerializeField] Iventory inventory;
    [SerializeField] GameObject inventoryCanvas;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        inventory.onItemChangeCallBack += UpdateUI;
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }

    void UpdateUI()
    {
        for (int i = 0;i < slots.Length; i++)
        {
            if (i < inventory.inventorySpace)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
        Debug.Log("Updating UI");
    }
}
