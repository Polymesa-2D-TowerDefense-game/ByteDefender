using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFocusAttacker : Attacker
{
    private EnemySpawner _spawner;

    // Start is called before the first frame update
    void Start()
    {
        _spawner = FindObjectOfType<EnemySpawner>();
        StartCoroutine(Attack());
    }

    // Update is called once per frame
    void Update()
    {
        GameEngine.LookAt(transform, GameEngine.GetMouseWorldPosition());
    }

    IEnumerator Attack()
    {
        while (true)
        {
            if (_spawner.IsWaveRunning())
            {
                // Spawn a projectile
                GameObject newProjectile = Instantiate(projectile, emmiter.position, emmiter.rotation);
                // Initialize projectile's variables and shoot
                GetComponent<AudioSource>().Play();
                newProjectile.GetComponent<PredirectedProjectile>().Initialize(
                    GameEngine.GetDifference(transform.position, GameEngine.GetMouseWorldPosition()),
                    projectileTravelSpeed, Damage, Range);
            }

            yield return new WaitForSeconds(1 / AttackSpeed);
        }
    }
}
