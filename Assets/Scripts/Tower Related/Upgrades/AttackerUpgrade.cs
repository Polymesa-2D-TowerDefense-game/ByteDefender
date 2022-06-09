using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="AttackerUpgrade", menuName ="Upgrades/new Attacker Upgrade", order= 1)]
public class AttackerUpgrade : TowerUpgrade
{
    [SerializeField]
    public float attackSpeedUpgrade;
    [SerializeField]
    public int damageUpgrade;
    [SerializeField]
    public float projectileTravelSpeedUpgrade;
    [SerializeField]
    public GameObject projectileUpgrade; 
}
