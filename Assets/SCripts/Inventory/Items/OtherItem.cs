using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Other Item", menuName = "Inventory System/Items/Other")]
public class OtherItem : ItemObject
{
    public void Awake()
    {
        type = ItemType.Other;
    }
}
