using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Item", fileName = "New Item")]
public class ItemSO : ScriptableObject, IItem
{

    [SerializeField] Sprite sprite;

    [SerializeField] bool applyOnCurrentPlayer = true;
    [SerializeField] float duration = 0f;
    [SerializeField] bool applyOnOtherPlayers = false;

    public Sprite GetSprite() { return sprite; }
    [SerializeField] int maxBombCountModifier = 1;
    [SerializeField] int explosionRangeModifier = 1;

    public void ApplyPowerUp(PlayerStats playerStats, bool isCurrentPlayer)
    {
        playerStats.maxBombCount = Mathf.Clamp(playerStats.maxBombCount + maxBombCountModifier, 1, 99);
        playerStats.explosionRange = Mathf.Clamp(playerStats.explosionRange + explosionRangeModifier, 1, 99);
    }
}


public interface IItem
{
    void ApplyPowerUp(PlayerStats player, bool isCurrentPlayer);
    Sprite GetSprite();
}