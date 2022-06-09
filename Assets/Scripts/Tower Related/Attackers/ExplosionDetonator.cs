using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDetonator : Attacker
{
    [SerializeField] LayerMask enemiesLayer;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        while (true)
        {
            Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, Range, enemiesLayer);
            enemiesInRange = GameEngine.FilterEnemiesInRange(enemiesInRange, CanSeeCryptedEnemies);
            if (enemiesInRange.Length > 0)
            {
                GameObject projectilePrefab = Instantiate(projectile, emmiter.position, transform.rotation);
                projectilePrefab.transform.localScale = new Vector2(Range, Range);
                projectilePrefab.GetComponent<Explosion>().Detonate(Range, Damage);
            }
            
            yield return new WaitForSeconds(1 / AttackSpeed);
        }
    }
}
