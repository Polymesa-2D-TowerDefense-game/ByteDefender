using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinFarmer : Tower, IUpgradeable
{
    [Header("Coin Farmer : Specific")]
    [SerializeField] GameObject coinPrefab;
    private GameObject _uiCoinGameObject;
    private PlayerWalletManager _playerWalletManager;

    [SerializeField] float productionSpeed;
    [SerializeField] int coinValue;

    public float ProductionSpeed { get { return productionSpeed; } set { productionSpeed = value; } }
    public int CoinValue { get { return coinValue; } set { coinValue = value; } }

    [Header("Farmer Upgrades")]
    [SerializeField] List<CoinFarmerUpgrade> leftUpgradePath;
    [SerializeField] List<CoinFarmerUpgrade> rightUpgradePath;

    private int _leftPathIndex = 0;
    private int _rightPathIndex = 0;

    private EnemySpawner _spawner;

    // Start is called before the first frame update
    void Start()
    {
        _uiCoinGameObject = GameObject.FindGameObjectWithTag("CoinIndicatorImage");
        _playerWalletManager = FindObjectOfType<PlayerWalletManager>();
        _spawner = FindObjectOfType<EnemySpawner>();
        StartCoroutine(ProduceCoins());
    }

    IEnumerator ProduceCoins()
    {
        while (true)
        {
            if(_spawner.IsWaveRunning())
            {
                GameObject coin = Instantiate(coinPrefab, transform.position, Quaternion.identity);
                coin.GetComponent<CollectableCoin>().InitializeCoin(coinValue, _playerWalletManager, _uiCoinGameObject);
            }
            yield return new WaitForSeconds(1 / ProductionSpeed);
        }
    }

    private void MakeUpgrade(CoinFarmerUpgrade upgrade)
    {
        if (upgrade.newTowerName != "")
            TowerName = upgrade.newTowerName;
        if (upgrade.newTowerDescription != "")
            TowerDescription = upgrade.newTowerDescription;
        if (upgrade.spriteUpgrade)
            GetComponent<SpriteRenderer>().sprite = upgrade.spriteUpgrade;

        Range += upgrade.rangeUpgrade;
        ProductionSpeed += upgrade.productionSpeed;
        CoinValue += upgrade.coinValue;
        Cost += upgrade.upgradeCost;
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
