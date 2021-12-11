using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField] int health = 10;

    public event Action onChange;
    public event Action onDie;

    public int DoDamage(int damageValue)
    {
        int result = Mathf.Max(0, damageValue - health);
        health = Mathf.Max(0, health - damageValue);
        Notify();
        return result;
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
        if (onDie != null && health <= 0)
            onDie();
    }
}
