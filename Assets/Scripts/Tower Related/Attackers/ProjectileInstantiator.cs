using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileInstantiator : Attacker
{
    [SerializeField] LayerMask enemiesLayer;
    [SerializeField] private bool isDepentedOnEnemiesInRange = false;
    private void Start()
    {
        StartCoroutine(AttackTarget());
    }

    // Attacks target every specified time
    IEnumerator AttackTarget()
    {
        while (true)
        {
            int numberOfEnemiesInRange = 0;
            if (isDepentedOnEnemiesInRange)
            {
                Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, Range, enemiesLayer);
                enemiesInRange = GameEngine.FilterEnemiesInRange(enemiesInRange,CanSeeCryptedEnemies);
                numberOfEnemiesInRange = enemiesInRange.Length;
            }
            else
            {
                numberOfEnemiesInRange = 1;
            }

            if(numberOfEnemiesInRange > 0)
            {
                GetComponent<AudioSource>().Play();
                GameObject projectilePrefab = Instantiate(projectile, emmiter.position, Quaternion.identity);
                projectilePrefab.GetComponent<Projectile>().Damage = Damage;
                projectilePrefab.GetComponent<Projectile>().TravelSpeed = projectileTravelSpeed;
                projectilePrefab.GetComponent<Projectile>().CanSeeCryptedEnemies = CanSeeCryptedEnemies;
            }
            yield return new WaitForSeconds(1 / AttackSpeed);
        }
    }
}
