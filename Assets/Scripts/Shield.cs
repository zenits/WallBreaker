using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{

    [SerializeField] int shield = 10;

    public event Action onChange;

    
    /// Return overload damage
    public int DoDamage(int damageValue)
    {
        int result = Mathf.Max(0, damageValue - shield);
        shield = Mathf.Max(0, shield - damageValue);
        Notify();
        return result;
    }

    public void Restore(int shieldValue)
    {
        shield += shieldValue;
        Notify();
    }

    public int GetShield()
    { return shield; }

    void Notify()
    {
        if (onChange != null)
            onChange();
    }
}
