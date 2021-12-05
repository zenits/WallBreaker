using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerData", fileName = "New PlayerStats")]
public class PlayerStats : ScriptableObject
{
    public int maxBombCount = 1;

    public int explosionRange = 1;

    [SerializeField] float defaultMoveSpeed = 5f;
    [SerializeField] float slowMoveSpeed = 2f;
    [SerializeField] float hightMoveSpeed = 8f;

    [SerializeField] List<ItemSO> powerUpList;

}
