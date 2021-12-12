using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : IntValue
{

    [SerializeField] int health = 10;

    public event Action onDie;

    public int DoDamage(int damageValue)
    {
        var result = base.Decrease(damageValue);
        if (GetHealth() == 0)
            onDie?.Invoke();
        return result;
    }

    public void Heal(int healValue)
    {
        base.Increase(healValue);
    }

    public int GetHealth()
    { return base.GetValue(); }

}
