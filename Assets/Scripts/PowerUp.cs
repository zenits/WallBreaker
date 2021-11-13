using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( menuName = "PowerUp" , fileName = "New PowerUp")]
public abstract class PowerUp : ScriptableObject
{
    [SerializeField] Sprite sprite;

    [SerializeField]  bool applyOnCurrentPlayer = true;
    [SerializeField]  bool applyOnOtherPlayers = false;

    public abstract void ApplyPowerUp(PlayerStats player, bool isCurrentPlayer);
}
