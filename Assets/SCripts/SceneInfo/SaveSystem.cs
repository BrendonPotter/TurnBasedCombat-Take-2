using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class SaveSystem : ScriptableObject
{
    
    public float _levelVar = 1;

    public int _expThreshVar = 100;
    public int _earnExpAmount = 0;

    public int hpAmount;
    public int maxHPAmount = 100;

    public int dealDamage;

    public int trueDamage;
    public int checkPoint;


    public float Value
	{
		get { return _levelVar; }
		set { _levelVar = value; }
	}
}

