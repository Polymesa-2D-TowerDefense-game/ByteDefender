using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CoinFarmerUpgrade", menuName = "Upgrades/new Coin Farmer Upgrade", order = 1)]
public class CoinFarmerUpgrade : TowerUpgrade
{
    [SerializeField]
    public float productionSpeed;
    [SerializeField]
    public int coinValue;
}
