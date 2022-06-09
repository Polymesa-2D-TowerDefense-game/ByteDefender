using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathObstacleInstantiator : Attacker
{
    [SerializeField] float obstacleOffset;
    private Vector2[] _obstacleSpawnPosition = new Vector2[3];

    private EnemySpawner _spawner;

    // Start is called before the first frame update
    void Start()
    {
        Vector2[]  path = FindObjectOfType<EnemyPathManager>().EnemyPath.ToArray();
        int index = GameEngine.GetNearestIndexToPoint(path, transform.position);
        _obstacleSpawnPosition[0] = path[index - 1];
        _obstacleSpawnPosition[1] = path[index];
        _obstacleSpawnPosition[2] = path[index + 1];
        _spawner = FindObjectOfType<EnemySpawner>();
        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        while (true)
        {
            if(_spawner.IsWaveRunning())
            {
                // Create Obstacle
                GameObject obstacle = Instantiate(projectile, emmiter.position, transform.rotation);
                // Initialize Projectile base stats
                obstacle.GetComponent<Projectile>().Damage = Damage;
                obstacle.GetComponent<Projectile>().TravelSpeed = projectileTravelSpeed;
                // Calculate target position
                float xOffset = Random.Range(-obstacleOffset, obstacleOffset);
                float yOffset = Random.Range(-obstacleOffset, obstacleOffset);
                int index = Random.Range(0, _obstacleSpawnPosition.Length);
                // Set Target position
                Vector2 position = _obstacleSpawnPosition[index] + new Vector2(xOffset, yOffset);
                GameEngine.LookAt(transform, position);
                obstacle.GetComponent<PathObstacle>().TargetPosition = position;
                GetComponent<AudioSource>().Play();
            }
            yield return new WaitForSeconds(1 / AttackSpeed);
        }
    }
}
