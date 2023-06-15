using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class SaveSystem : ScriptableObject
{
    
    public float _levelVar = 1;

    public int _expThreshVar = 100;


    public float Value
	{
		get { return _levelVar; }
		set { _levelVar = value; }
	}

}

