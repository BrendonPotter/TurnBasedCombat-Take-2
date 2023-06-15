using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LightningStrike : MonoBehaviour
{
    public float strikeTime = 1f;
    private float elapsedTime = 0f;

    private Unit targetUnit;

    public void SetTarget(Unit target)
    {
        targetUnit = target;
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(strikeTime);

        bool isDead = targetUnit.TakeDamage(10); // Modify the damage value as desired

        if (isDead)
        {
            // Handle enemy defeat
        }

        Destroy(gameObject);
    }

    private void Update()
    {
        
        // Check if the arrow has reached the target position
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= strikeTime)
        {
            Destroy(gameObject);
        }
    }
}
