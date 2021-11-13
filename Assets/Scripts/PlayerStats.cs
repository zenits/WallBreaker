using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : ScriptableObject
{
    [SerializeField] int maxBombCount = 1;
    int activeBombCount = 0;

    [SerializeField] int explosionRange = 1;

    [SerializeField] float defaultMoveSpeed = 2.5f;
    [SerializeField] float slowMoveSpeed = 5f;
    [SerializeField] float hightMoveSpeed = 8f;

}
