using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IDamageable
{
    public ItemSO item;

    public void GetDamage(int damageValue)
    {
        Destroy(gameObject);
    }
}
