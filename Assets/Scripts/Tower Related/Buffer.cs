using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buffer : Tower, IUpgradeable
{
    [Header("Buffer Specific Stats")]
    [SerializeField]
    LayerMask towersLayer;
    [SerializeField]
    GameObject buffPrefab;
    [SerializeField]
    private float buffDuration;
    [SerializeField]
    private int damageBuff;
    [SerializeField]
    private float attackSpeedBuff;
    [SerializeField]
    private float rangeBuff;

    [Header("Attacker Upgrades")]
    [SerializeField] List<BufferUpgrade> leftUpgradePath;
    [SerializeField] List<BufferUpgrade> rightUpgradePath;

    private int _leftPathIndex = 0;
    private int _rightPathIndex = 0;

    public float BuffDuration { get { return buffDuration; } set { buffDuration = value; } }
    public int DamageBuff { get { return damageBuff; } set { damageBuff = value; } }
    public float AttackSpeedBuff { get { return attackSpeedBuff; } set { attackSpeedBuff = value; } }
    public float RangeBuff { get { return rangeBuff; } set { rangeBuff = value; } }

    private void Start()
    {
        StartCoroutine(BuffNearbyTowers());
    }

    private void MakeUpgrade(BufferUpgrade upgrade)
    {
        if (upgrade.newTowerName != "")
            TowerName = upgrade.newTowerName;
        if (upgrade.newTowerDescription != "")
            TowerDescription = upgrade.newTowerDescription;
        if (upgrade.spriteUpgrade)
            GetComponent<SpriteRenderer>().sprite = upgrade.spriteUpgrade;

        BuffDuration += upgrade.buffDurationUpgrade;
        Range += upgrade.rangeUpgrade;
        DamageBuff += upgrade.damageBuffUpgrade;
        attackSpeedBuff += upgrade.attackSpeedBuffUpgrade;
        rangeBuff += upgrade.rangeBuffUpgrade;
        Cost += upgrade.upgradeCost;
    }

    public int GetLeftUpgradeIndex()
    {
        return _leftPathIndex;
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

    public int GetRightUpgradeIndex()
    {
        return _rightPathIndex;
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

    IEnumerator BuffNearbyTowers()
    {
        while (true)
        {
            Collider2D[] towersInRange = Physics2D.OverlapCircleAll(transform.position, Range, towersLayer);
            foreach(var tower in towersInRange)
            {
                if(tower.GetComponent<Attacker>())
                {
                    GameObject buff = Instantiate(buffPrefab, tower.transform.position, Quaternion.identity, tower.transform);
                    buff.GetComponent<AttackerBuff>().Initialize(tower.GetComponent<Attacker>(), BuffDuration,
                        DamageBuff, AttackSpeedBuff, RangeBuff);
                }
            }
            yield return new WaitForSeconds(2f);
        }
    }
}
