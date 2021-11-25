using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField] int health = 10;

    public event Action onChange;

    public void DoDamage(int damageValue)
    {
        health -= damageValue;
        Notify();
    }

    public void Heal(int healValue)
    {
        health += healValue;
        Notify();
    }

    public int GetHealth()
    { return health; }

    void Notify()
    {
        if (onChange != null)
            onChange();
    }
}
