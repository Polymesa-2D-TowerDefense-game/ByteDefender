using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : Tower, IUpgradeable
{
    [Header("Attacker Specific Stats")]
    [SerializeField]
    public GameObject projectile;
    [SerializeField]
    public float projectileTravelSpeed;
    [SerializeField]
    public Transform emmiter;
    [SerializeField]
    private int damage;
    [SerializeField]
    private float attackSpeed;

    [Header("Attacker Upgrades")]
    [SerializeField] List<AttackerUpgrade> leftUpgradePath;
    [SerializeField] List<AttackerUpgrade> rightUpgradePath;

    private int _leftPathIndex = 0;
    private int _rightPathIndex = 0;

    public int Damage { get { return damage; } set { damage = value; } }
    public float AttackSpeed { get { return attackSpeed; } set { attackSpeed = value; } }

    public bool CanSeeCryptedEnemies = false;

    private void MakeUpgrade(AttackerUpgrade upgrade)
    {
        if(upgrade.newTowerName != "")
            TowerName = upgrade.newTowerName;
        if (upgrade.newTowerDescription != "")
            TowerDescription = upgrade.newTowerDescription;
        if (upgrade.spriteUpgrade)
            GetComponent<SpriteRenderer>().sprite = upgrade.spriteUpgrade;
        if (upgrade.projectileUpgrade)
            projectile = upgrade.projectileUpgrade;

        Range += upgrade.rangeUpgrade;
        Damage += upgrade.damageUpgrade;
        AttackSpeed += upgrade.attackSpeedUpgrade;
        projectileTravelSpeed += upgrade.projectileTravelSpeedUpgrade;
        Cost += upgrade.upgradeCost;

        UpdateRangeIndicator();
    }

    public void LeftPathUpgrade()
    {
        // If upgrade Index exceeds array length
        if (_leftPathIndex >= leftUpgradePath.Count)
            return;

        // Get current upgrade and player wallet
        var currentUpgrade = leftUpgradePath[_leftPathIndex];
        var playerWallet = FindObjectOfType<PlayerWalletManager>();

        // If player has not enough coins for the upgrade
        if (!playerWallet.CanPurchase(currentUpgrade.upgradeCost))
            return;

        // Purchase upgrade and make the required changes
        playerWallet.Purchase(currentUpgrade.upgradeCost);
        MakeUpgrade(currentUpgrade);
        _leftPathIndex++;

        if (_leftPathIndex >= leftUpgradePath.Count)
            rightUpgradePath.Remove(rightUpgradePath[rightUpgradePath.Count - 1]);
    }

    public void RightPathUpgrade()
    {
        // If upgrade Index exceeds array length
        if (_rightPathIndex >= rightUpgradePath.Count)
            return;

        // Get current upgrade and player wallet
        var playerWallet = FindObjectOfType<PlayerWalletManager>();
        var currentUpgrade = rightUpgradePath[_rightPathIndex];

        // If player has not enough coins for the upgrade
        if (!playerWallet.CanPurchase(currentUpgrade.upgradeCost))
            return;

        // Purchase upgrade and make the required changes
        playerWallet.Purchase(currentUpgrade.upgradeCost);
        MakeUpgrade(currentUpgrade);
        _rightPathIndex++;

        if (_rightPathIndex >= rightUpgradePath.Count)
            leftUpgradePath.Remove(leftUpgradePath[rightUpgradePath.Count - 1]);
    }

    public string GetLeftUpgradeName()
    {
        if (_leftPathIndex >= leftUpgradePath.Count)
            return "Max";
        return leftUpgradePath[_leftPathIndex].upgradeName;
    }

    public string GetLeftUpgraDescription()
    {
        if (_leftPathIndex >= leftUpgradePath.Count)
            return "This path is maxed out";
        return leftUpgradePath[_leftPathIndex].upgradeDescription;
    }

    public int GetLeftUpgradeIndex()
    {
        return _leftPathIndex;
    }

    public string GetRightUpgradeName()
    {
        if (_rightPathIndex >= rightUpgradePath.Count)
            return "Max";
        return rightUpgradePath[_rightPathIndex].upgradeName;
    }

    public string GetRightUpgraDescription()
    {
        if (_rightPathIndex >= rightUpgradePath.Count)
            return "This path is maxed out";
        return rightUpgradePath[_rightPathIndex].upgradeDescription;
    }

    public int GetRightUpgradeIndex()
    {
        return _rightPathIndex;
    }

    public int GetLeftUpgradeCost()
    {
        if (_leftPathIndex >= leftUpgradePath.Count)
            return 0;
        return leftUpgradePath[_leftPathIndex].upgradeCost;
    }
    public int GetRightUpgradeCost()
    {
        if (_rightPathIndex >= rightUpgradePath.Count)
            return 0;
        return rightUpgradePath[_rightPathIndex].upgradeCost;
    }
}
