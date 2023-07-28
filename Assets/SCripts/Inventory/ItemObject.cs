using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Food,
    Equipment,
    Other,
}
public class ItemObject : ScriptableObject
{
    public GameObject preFab;
    public ItemType type;
    [TextArea(15, 20)]
    public string Description;
}