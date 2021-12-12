using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : IntValue
{
    
    /// Return overload damage
    public int DoDamage(int damageValue)
    {
        var result = base.Decrease(damageValue);
        return result;
    }

    public void Restore(int shieldValue)
    {
        base.Increase(shieldValue);
    }

    public int GetShield()
    { return GetValue(); }

}
