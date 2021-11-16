using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu( menuName = "PowerUp/Bomb" , fileName = "New Bomb")]
public class ItemSO : AbstractItemSO
{

    [SerializeField] int maxBombCountModifier = 1;
    [SerializeField] int explosionRangeModifier = 1;
    public override void ApplyPowerUp(PlayerStats player, bool isCurrentPlayer)
    {
        player.maxBombCount += maxBombCountModifier;
        player.explosionRange += explosionRangeModifier;
    }
}