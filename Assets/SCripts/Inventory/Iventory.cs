using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory")]
public class Iventory : ScriptableObject
{
    //When something change on the inventory i.e: add or remove items.
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangeCallBack;

    //List to store items
    public List<ItemData> items = new List<ItemData>();
    public static Iventory instance;

    //Iventory Space
    public int inventorySpace = 20;


    #region Singleton
    private void Awake()
    {
        if (instance!= null)
        {
            Debug.LogWarning("More than one instance of Iventory found");
            return;
        }
        instance = this;
    }
    #endregion
    public bool AddItem(ItemData item)
    {
        if(!item.isDefaultItem)
        {
            if (items.Count >= inventorySpace)
            {
                Debug.Log("Not Enough Room");
                return false;
            }
            items.Add(item);

            if (onItemChangeCallBack != null)
            {
                onItemChangeCallBack.Invoke();
            }
        }
        return true;
    }

    public void RemoveItem(ItemData item)
    {
        items.Remove(item);

        if (onItemChangeCallBack != null)
        {
            onItemChangeCallBack.Invoke();
        }
    }
}
