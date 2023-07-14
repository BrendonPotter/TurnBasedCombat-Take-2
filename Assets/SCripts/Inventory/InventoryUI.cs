using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory;

    public int xStart;
    public int yStart;
    public int x_Space_Between_Item;
    public int y_Space_Between_Item;
    public int numberOfColumn;
    Dictionary<Inventory, GameObject> itemsDisplayed = new Dictionary<Inventory, GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        CreateDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateDisplay();
    }

    public void CreateDisplay()
    {
        for (int i = 0; i < inventory.Items.Count; i++)
        {
            var obj = Instantiate(inventory.Items[i].item.preFab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Items[i].amounts.ToString("n0");
            //itemsDisplayed.Add(inventory.Items[i], obj);
        }
    }

    public Vector3 GetPosition(int i)
    {
        return new Vector3(xStart + (x_Space_Between_Item * (i % numberOfColumn)), yStart - (y_Space_Between_Item * (i / numberOfColumn)), 0f);
    }

    //public void UpdateDisplay()
    //{
    //    for (int i = 0; i < inventory.Items.Count; i++)
    //    {
    //        if (itemsDisplayed.ContainsKey(inventory.Items[i]))
    //        {
    //            itemsDisplayed[inventory.Items[i]].GetComponentInChildren<TextMeshProUGUI>().text = inventory.Items[i].amounts.ToString("n0");
    //        }
    //        else
    //        {
    //            var obj = Instantiate(inventory.Items[i].item.preFab, Vector3.zero, Quaternion.identity, transform);
    //            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
    //            obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Items[i].amounts.ToString("n0");
    //            itemsDisplayed.Add(inventory.Items[i], obj);
    //        }
    //    }
    //}

}
