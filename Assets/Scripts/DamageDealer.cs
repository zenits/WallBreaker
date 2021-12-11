using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{

    [SerializeField] int damageValue = 1;
    [SerializeField] bool damageOverTime = false;
    [SerializeField] float damageTick = 0.5f;

    float elapsedTime = 0f;
    bool damageDealed = false;
    bool dealingDamage = false;


    private void OnTriggerEnter2D(Collider2D other)
    {
        ApplyDamage(other.gameObject);
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        ApplyDamage(other.gameObject);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        dealingDamage = false;
    }


    private void Update()
    {
        if (dealingDamage)
            elapsedTime += Time.deltaTime;
    }

    void ApplyDamage(GameObject obj)
    {
        dealingDamage = true;
        if (damageDealed && !damageOverTime)
        {
            return;
        }

        IDamageable idam = obj.GetComponent<IDamageable>();
        if (idam != null)
        {
            idam.GetDamage(damageValue);
            damageDealed = true;
        }
    }
}

public interface IDamageable
{
    void GetDamage(int damageValue);
}