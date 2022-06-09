using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BufferUpgrade", menuName = "Upgrades/new Buffer Upgrade", order = 1)]
public class BufferUpgrade : TowerUpgrade
{
    [SerializeField]
    public float buffDurationUpgrade;
    [SerializeField]
    public int damageBuffUpgrade;
    [SerializeField]
    public float attackSpeedBuffUpgrade;
    [SerializeField]
    public float rangeBuffUpgrade;
}
