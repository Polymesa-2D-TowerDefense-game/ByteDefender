using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Tower : MonoBehaviour, IPointerClickHandler
{
    [Header("General Tower Information")]
    [SerializeField]
    private string towerName, towerDescription;
    public GameObject TowerRangeIndicator;
    [SerializeField]
    private int cost;
    [Header("General Tower Stats")]
    [SerializeField]
    private float range;

    

    // Indicates if tower is placed and active (so it can attack)
    private bool isActive = false;
    private GameObject _playerManagerObject;

    public string TowerName { get { return towerName; } set { towerName = value; } }
    public string TowerDescription { get { return towerDescription; } set { towerDescription = value; } }
    public int Cost { get { return cost; } set { cost = value; } }
    public float Range { get { return range; } set { range = value; } }

    private void Awake()
    {
        _playerManagerObject = GameObject.Find("Player");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _playerManagerObject.GetComponent<PlayerTargetHandler>().UpdateTarget(this.gameObject);
        UpdateRangeIndicator();
    }

    public void UpdateRangeIndicator()
    {
        TowerRangeIndicator.transform.localScale =  new Vector2(Range,Range);
    }

}
