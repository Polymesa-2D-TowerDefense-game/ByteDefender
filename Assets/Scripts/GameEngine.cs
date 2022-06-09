using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public static class GameEngine
{
    #region General Functions

    public static int GetNearestIndexToPoint(Vector2[] pointsArray, Vector2 point)
    {
        float minDistance = Mathf.Infinity;
        int minimumDistanceIndex = 0;

        for (int i = 0; i < pointsArray.Length; i++)
        {
            float calculatedDistance = Vector2.Distance(pointsArray[i], point);
            if (calculatedDistance < minDistance)
            {
                minDistance = calculatedDistance;
                minimumDistanceIndex = i;
            }
        }

        return minimumDistanceIndex;
    }

    #endregion

    #region UI Functions

    public static Vector2 GetMouseWorldPosition()
    {
        Vector2 mousePosition = Input.mousePosition;
        Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        return mouseWorldPosition;
    }

    public static bool IsMouseOverLayer(int layer)
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> raycastResultList = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResultList);

        for (int i = 0; i < raycastResultList.Count; i++)
            if (raycastResultList[i].gameObject.layer == layer)
                return true;

        return false;
    }

    public static bool IsMouseOutsideCameraView()
    {
        var view = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        var isOutside = view.x < 0 || view.x > 1 || view.y < 0 || view.y > 1;

        return isOutside;
    }

    #endregion

    #region Tower Functions
    // Look at Target
    public static void LookAt(Transform observer, Vector2 targetPosition)
    {
        Vector2 difference = targetPosition - (Vector2)observer.position;
        difference.Normalize();
        float zRotation = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        observer.rotation = Quaternion.Euler(0f, 0f, zRotation - 90f);
    }

    public static Vector2 GetDifference(Vector2 position, Vector2 target)
    {
        Vector2 difference = target - position;
        return difference;
    }

    // Get Closest Target to Point
    public static GameObject GetClosestTargetToPoint(Collider2D[] targets, Vector2 point)
    {
        float closestDistance = Mathf.Infinity;
        GameObject closestTarget = null;

        foreach(Collider2D potentialTarget in targets)
        {
            Vector2 directionToTarget = (Vector2)potentialTarget.transform.position - point;
            float distanceToTarget = directionToTarget.sqrMagnitude;
            if(distanceToTarget < closestDistance)
            {
                closestDistance = distanceToTarget;
                closestTarget = potentialTarget.gameObject;
            }
        }

        return closestTarget;
    }

    // Get Furthest Target to Point
    public static GameObject GetFurthestTargetToPoint(Collider2D[] targets, Vector2 point)
    {
        float furthestDistance = Mathf.NegativeInfinity;
        GameObject furthestTarget = null;

        foreach (Collider2D potentialTarget in targets)
        {
            Vector2 directionToTarget = (Vector2)potentialTarget.transform.position - point;
            float distanceToTarget = directionToTarget.sqrMagnitude;
            if (distanceToTarget > furthestDistance)
            {
                furthestDistance = distanceToTarget;
                furthestTarget = potentialTarget.gameObject;
            }
        }

        return furthestTarget;
    }

    public static Collider2D[] FilterEnemiesInRange(Collider2D[] enemiesInRange, bool canSeeCryptedEnemies)
    {
        // If tower can see crypted enemies return the list as it is
        if (canSeeCryptedEnemies)
            return enemiesInRange;
        // If not, create an empty list
        List<Collider2D> visibleEnemiesInRange = new List<Collider2D>();
        // Populate this list with enemies that are not Crypted
        foreach (Collider2D enemy in enemiesInRange)
        {
            if (!enemy.GetComponent<CryptedEnemy>())
                visibleEnemiesInRange.Add(enemy);
        }
        // Return the filtered list as an array
        return visibleEnemiesInRange.ToArray();
    }

    #endregion

    #region Tilemap Functions

    // Returns the world position of each tile on the selected tilemap
    public static List<Vector2> GetTilePositions(Tilemap tilemap)
    {
        List<Vector2> tilePositions = new List<Vector2>();

        foreach (var cell in tilemap.cellBounds.allPositionsWithin)
        {
            if (tilemap.HasTile(cell))
            {
                var worldPos = tilemap.CellToWorld(cell) + tilemap.tileAnchor;
                tilePositions.Add(worldPos);
            }
        }

        return tilePositions;
    }

    #endregion
}
