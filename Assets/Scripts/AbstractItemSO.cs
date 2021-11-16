using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class AbstractItemSO : ScriptableObject
{
    [SerializeField] Sprite sprite;

    [SerializeField]  bool applyOnCurrentPlayer = true;
    [SerializeField]  float duration = 0f;
    [SerializeField]  bool applyOnOtherPlayers = false;

    public abstract void ApplyPowerUp(PlayerStats player, bool isCurrentPlayer);

    public Sprite GetSprite() {return sprite;}
}


