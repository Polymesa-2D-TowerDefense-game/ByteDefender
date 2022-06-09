using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopTowerListHandler : MonoBehaviour
{
    [SerializeField]
    GameObject[] towerPrefabs;
    [SerializeField]
    GameObject towerShopPlaceholderPrefab;
    [SerializeField]
    Transform towerContentTransform;
    [SerializeField]
    Transform canvasTransform;
    [SerializeField]
    string towerNameTextObjectName, towerCostTextObjectName;

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject tower in towerPrefabs)
        {
            // Create tower UI Image from prefab
            GameObject towerInstance = Instantiate(towerShopPlaceholderPrefab, towerContentTransform);
            var towerInstanceDragHandler = towerInstance.GetComponentInChildren<TowerDragHandler>();
            // Set its tower prefab
            towerInstanceDragHandler.OnDragParent = canvasTransform;
            towerInstanceDragHandler.SetTowerPrefab(tower);
            // Get its name and cost text fields
            var towerNameTextObject = towerInstance.transform.Find(towerNameTextObjectName).GetComponent<TextMeshProUGUI>();
            var towerCostObject = towerInstance.transform.Find(towerCostTextObjectName).GetComponent<TextMeshProUGUI>();
            // Set towers name and cost
            towerNameTextObject.text = tower.GetComponent<Tower>().TowerName;
            towerCostObject.text = tower.GetComponent<Tower>().Cost.ToString();
        }
    }
}
