using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUpgradeable
{
    public void LeftPathUpgrade();
    public void RightPathUpgrade();
    public string GetLeftUpgradeName();
    public string GetLeftUpgraDescription();
    public int GetLeftUpgradeIndex();
    public string GetRightUpgradeName();
    public string GetRightUpgraDescription();
    public int GetRightUpgradeIndex();
    public int GetLeftUpgradeCost();
    public int GetRightUpgradeCost();
}
