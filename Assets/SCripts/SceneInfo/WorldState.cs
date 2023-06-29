using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class WorldState : ScriptableObject
{
    public bool obtainKey;


    public bool Value
    {
        get { return obtainKey; }
        set { obtainKey = value; }
    }
}


