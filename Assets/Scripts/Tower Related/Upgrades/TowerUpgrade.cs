using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUpgrade : ScriptableObject
{
    [SerializeField]
    public string newTowerName;
    [SerializeField]
    public string newTowerDescription;
    [SerializeField]
    public int upgradeCost;
    [SerializeField]
    public string upgradeName;
    [SerializeField]
    public string upgradeDescription;
    [SerializeField] 
    public float rangeUpgrade;
    [SerializeField]
    public Sprite spriteUpgrade;

}
