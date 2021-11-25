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

    private void OnCollisionEnter2D(Collision2D other)
    {
        ApplyDamage(other.gameObject);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        ApplyDamage(other.gameObject);
    }
    private void OnCollisionExit2D(Collision2D other)
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
        var health = obj.GetComponent<Health>();
        if (health != null)
        {

            //if (!damageOverTime)
            health.DoDamage(damageValue);

            damageDealed = true;
        }
    }
}