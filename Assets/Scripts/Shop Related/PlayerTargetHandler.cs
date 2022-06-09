using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerTargetHandler : MonoBehaviour
{
    private GameObject _currentTarget;

    // Information Panel Objects
    [SerializeField]
    private GameObject targetInfoPanel;
    [SerializeField]
    private Image targetImage;
    [SerializeField]
    private TextMeshProUGUI targetNameText;
    [SerializeField]
    private TextMeshProUGUI targetDescriptionText;
    [Header("Upgrade Related Objects")]
    [SerializeField]
    private TextMeshProUGUI leftUpgradePathNameText;
    [SerializeField]
    private TextMeshProUGUI leftUpgradePathDescriptionText;
    [SerializeField]
    private TextMeshProUGUI leftUpgradePathCostText;
    [SerializeField]
    private Slider leftUpgradeSlider;
    [SerializeField]
    private TextMeshProUGUI rightUpgradePathNameText;
    [SerializeField]
    private TextMeshProUGUI rightUpgradePathDescriptionText;
    [SerializeField]
    private TextMeshProUGUI rightUpgradePathCostText;
    [SerializeField]
    private Slider rightUpgradeSlider;
    [SerializeField]
    private PlayerWalletManager walletManager;

    // Updates the player's target
    public void UpdateTarget(GameObject newTarget)
    {
        // Return if there is no valid target
        if (!newTarget)
            return;
        // If player had a target previously, disable its range indicator
        RemoveTarget();
        // Update Target and show its range indicator
        _currentTarget = newTarget;
        _currentTarget.GetComponent<Tower>().TowerRangeIndicator.SetActive(true);
        UpdateTargetInfo();
        targetInfoPanel.SetActive(true);
    }

    // Removes target and disables the information panel
    public void RemoveTarget()
    {
        if(_currentTarget)
        {
            _currentTarget.GetComponent<Tower>().TowerRangeIndicator.SetActive(false);
            targetInfoPanel.SetActive(false);
            _currentTarget = null;
        }
    }

    // Update Target's Information Panel
    private void UpdateTargetInfo()
    {
        if (!_currentTarget)
            return;

        Tower targetedTower = _currentTarget.GetComponent<Tower>();

        targetImage.sprite = _currentTarget.GetComponent<SpriteRenderer>().sprite;
        targetNameText.text = targetedTower.TowerName;
        targetDescriptionText.text = targetedTower.TowerDescription;
        // Upgrade Paths Info
        IUpgradeable targetedTowerUpgradeInfo = _currentTarget.GetComponent<IUpgradeable>();
        // Left Path
        leftUpgradePathNameText.text = targetedTowerUpgradeInfo.GetLeftUpgradeName();
        leftUpgradePathDescriptionText.text = targetedTowerUpgradeInfo.GetLeftUpgraDescription();
        leftUpgradeSlider.value = targetedTowerUpgradeInfo.GetLeftUpgradeIndex();
        leftUpgradePathCostText.text = targetedTowerUpgradeInfo.GetLeftUpgradeCost().ToString() + "G";
        // Right Path
        rightUpgradePathNameText.text = targetedTowerUpgradeInfo.GetRightUpgradeName();
        rightUpgradePathDescriptionText.text = targetedTowerUpgradeInfo.GetRightUpgraDescription();
        rightUpgradeSlider.value = targetedTowerUpgradeInfo.GetRightUpgradeIndex();
        rightUpgradePathCostText.text = targetedTowerUpgradeInfo.GetRightUpgradeCost().ToString() + "G";
    }

    public void MakeLeftUpgrade()
    {
        _currentTarget.GetComponent<IUpgradeable>().LeftPathUpgrade();
        UpdateTargetInfo();
    }

    public void MakeRightUpgrade()
    {
        _currentTarget.GetComponent<IUpgradeable>().RightPathUpgrade();
        UpdateTargetInfo();
    }

    public void DeleteTarget()
    {
        if(_currentTarget)
        {
            Destroy(_currentTarget.gameObject);
            walletManager.AddCoins((int)(_currentTarget.GetComponent<Tower>().Cost/ 1.5f));
            targetInfoPanel.SetActive(false);
        }
            
    }
}
