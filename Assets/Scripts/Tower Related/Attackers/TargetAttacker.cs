using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAttacker : Attacker
{
    [Header("Target Properties")]
    [SerializeField]
    LayerMask enemiesLayer;
    [SerializeField]
    public TargetPriority priority;

    

    private void Start()
    {
        StartCoroutine(AttackTarget());
    }

    

    // Focus target based on selected priority and tower's range
    private GameObject GetFocusTarget()
    {
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, Range, enemiesLayer);
        enemiesInRange = GameEngine.FilterEnemiesInRange(enemiesInRange, CanSeeCryptedEnemies);

        GameObject target = null;

        switch (priority)
        {
            case TargetPriority.closest:
                target = GameEngine.GetClosestTargetToPoint(enemiesInRange, transform.position);
                break;
            case TargetPriority.furthest:
                target = GameEngine.GetFurthestTargetToPoint(enemiesInRange, transform.position);
                break;
            case TargetPriority.stronger:
                break;
            default:
                break;
        }

        return target;
    }

    // Attacks target every specified time
    IEnumerator AttackTarget()
    {
        while(true)
        {
            GameObject target = GetFocusTarget();
            if(target)
            {
                // Look at target
                GameEngine.LookAt(transform, target.transform.position);
                // Spawn a projectile
                GameObject newProjectile = Instantiate(projectile, emmiter.position, Quaternion.identity);
                // Initialize projectile's variables and shoot
                GetComponent<AudioSource>().Play();
                newProjectile.GetComponent<PredirectedProjectile>().Initialize(
                    GameEngine.GetDifference(transform.position, target.transform.position),
                    projectileTravelSpeed, Damage,Range);
            }
            yield return new WaitForSeconds(1 / AttackSpeed);
        }
    }
}

// Target Priority 
public enum TargetPriority
{
    closest,
    furthest,
    stronger
}
