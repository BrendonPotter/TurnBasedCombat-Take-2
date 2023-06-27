using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLevel;

    public int damage;
    public int trueDamage;

    public int maxHP;
    public int currentHP;

    public int Checkpoint = 1;

    

    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;

        if(currentHP <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
