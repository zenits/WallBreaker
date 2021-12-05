using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPicker : MonoBehaviour
{

    Player player;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Item")
        {
            // todo raise event OnItemPicked(Iitem item)
            (other.gameObject).GetComponent<Item>().item.ApplyPowerUp(player.GetStats(), true);
            Destroy(other.gameObject);
        }
    }
}
