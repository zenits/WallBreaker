using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerData", fileName = "New PlayerData")]
public class PlayerStats : ScriptableObject
{
    [SerializeField]public int maxBombCount {get;set;}= 1;
    int activeBombCount = 0;

    [SerializeField] public int explosionRange {get;set;}= 1;

    [SerializeField] float defaultMoveSpeed = 2.5f;
    [SerializeField] float slowMoveSpeed = 5f;
    [SerializeField] float hightMoveSpeed = 8f;

    [SerializeField] List<AbstractItemSO> powerUpList;

}
