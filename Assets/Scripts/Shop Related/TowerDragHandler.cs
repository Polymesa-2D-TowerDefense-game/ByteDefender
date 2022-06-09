using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TowerDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Transform OnDragParent{get; set;}
    private GameObject _towerPrefab;

    private Transform _parentObject = null;

    private GameObject _playerManagerObject;
    private PlayerWalletManager _playerWalletManager;

    private void Start()
    {
        _playerManagerObject = GameObject.Find("Player");
        _playerWalletManager = _playerManagerObject.GetComponent<PlayerWalletManager>();
        _parentObject = transform.parent;
    }

    // Set the UI image's tower relation and update its sprite
    public void SetTowerPrefab(GameObject towerPrefab)
    {
        _towerPrefab = towerPrefab;
        GetComponent<Image>().sprite = _towerPrefab.GetComponent<SpriteRenderer>().sprite;
    }

    // When tower's image is being dragged
    public void OnDrag(PointerEventData eventData)
    {
        // Follow mouse position
        transform.position = Input.mousePosition;
        transform.SetParent(OnDragParent);
    }

    // When tower's image stops being dragged (on mouse left release)
    public void OnEndDrag(PointerEventData eventData)
    {
        // Set its parent to be its original one and its local position to 0
        transform.SetParent(_parentObject);
        transform.localPosition = Vector3.zero;

        // If player has enough coins purchase the selected tower
        if (_playerWalletManager.CanPurchase(_towerPrefab.GetComponent<Tower>().Cost))
        {
            
            // Create tower prefab at that point (in the game world)
            if (!GameEngine.IsMouseOverLayer(9) && !GameEngine.IsMouseOverLayer(8) && !GameEngine.IsMouseOverLayer(7) && !GameEngine.IsMouseOverLayer(6) && !GameEngine.IsMouseOutsideCameraView())
            {
                GameObject towerPrefab = Instantiate(_towerPrefab, GameEngine.GetMouseWorldPosition(), Quaternion.identity);
                _playerManagerObject.GetComponent<PlayerTargetHandler>().UpdateTarget(towerPrefab);
                _playerWalletManager.Purchase(_towerPrefab.GetComponent<Tower>().Cost);
            }
        }
    }

    // Show tower info when pointer enters object
    public void OnPointerEnter(PointerEventData eventData)
    {
        _playerManagerObject.GetComponent<PlayerTargetHandler>().UpdateTarget(_towerPrefab);
    }

    // Diselect target when pointer exit's object
    public void OnPointerExit(PointerEventData eventData)
    {
        _playerManagerObject.GetComponent<PlayerTargetHandler>().RemoveTarget();
    }
}
