using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyPathManager : MonoBehaviour
{
    [SerializeField]
    Tilemap enemyPathTilemap;
    [SerializeField]
    Transform enemySpawnerPosition;

    private Vector2[] _enemyPathPositions;

    public List<Vector2> EnemyPath { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        EnemyPath = GetSortedEnemyPath();
    }

    // Sorts the enemy path and saves it in a list
    private List<Vector2> GetSortedEnemyPath()
    {
        // Get all cells of the enemy path tilemap and conver it to array
        _enemyPathPositions = GameEngine.GetTilePositions(enemyPathTilemap).ToArray();
        // Get closest tilemap cell position index to enemy spawner
        int startingIndex = GameEngine.GetNearestIndexToPoint(_enemyPathPositions, enemySpawnerPosition.position);

        // Create an empty list to store the sorted path
        List<Vector2> sortedPath = new List<Vector2>();
        // Add the starting point to the list
        sortedPath.Add(_enemyPathPositions[startingIndex]);
        // Set the added cell's position to infinite so it cant be recognized as a closest point
        _enemyPathPositions[startingIndex] = Vector2.positiveInfinity;

        // Do the above process for each cell
        while (sortedPath.Count < _enemyPathPositions.Length)
        {
            int closestIndex = GameEngine.GetNearestIndexToPoint(_enemyPathPositions, sortedPath[sortedPath.Count - 1]);
            sortedPath.Add(_enemyPathPositions[closestIndex]);
            _enemyPathPositions[closestIndex] = Vector2.positiveInfinity;
        }

        return sortedPath;
    }
}
