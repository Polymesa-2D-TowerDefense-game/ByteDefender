using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZippedEnemy : Enemy, IDamagable
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float spawningOffset;

    private int _enemiesToSpawnOnDeath;

    private void Start()
    {
        _enemiesToSpawnOnDeath = (int)(Health / 2f);
    }

    public void TakeDamage(int damage)
    {
        if(Health - damage <= 0)
        {
            SpawnEnemiesBeforeDeath();
        }
        LooseHealth(damage);
    }

    private void SpawnEnemiesBeforeDeath()
    {
        for (int i = 0; i < _enemiesToSpawnOnDeath; i++)
        {
            // Randomize Offset both X and Y
            float randomXOffset = Random.Range(-spawningOffset, spawningOffset);
            float randomYOffset = Random.Range(-spawningOffset, spawningOffset);
            // Choose an index between the previous and the next one
            int randomIndex = Mathf.Clamp(Random.Range(_pathIndex - 1, _pathIndex + 1), 0, _path.Count - 1);
            // Set enemy position to be the randomIndex + offset postion
            Vector2 spawnPosition = _path[randomIndex] + new Vector2(randomXOffset, randomYOffset);
            // Spawn enemy and set it's path
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            newEnemy.GetComponent<Enemy>().SetPath(_path, randomIndex);
            newEnemy.GetComponent<Enemy>().MoveSpeed = MoveSpeed;
            newEnemy.GetComponent<Enemy>().Coins = Coins;
            newEnemy.GetComponent<Enemy>().PlayerWallet = PlayerWallet;
            newEnemy.GetComponent<Enemy>().PlayerHealthManager = PlayerHealthManager;
        }
    }
}
