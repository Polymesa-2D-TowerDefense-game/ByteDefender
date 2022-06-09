using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingProjectile : Projectile
{
    [SerializeField] LayerMask enemiesLayer;
    public GameObject Target { get; set; }

    private void Update()
    {
        if(!Target)
        {
            Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, Mathf.Infinity, enemiesLayer);
            enemiesInRange = GameEngine.FilterEnemiesInRange(enemiesInRange, CanSeeCryptedEnemies);
            Target = GameEngine.GetClosestTargetToPoint(enemiesInRange, transform.position);
        }
        else
            transform.position = Vector2.MoveTowards(transform.position, Target.transform.position, TravelSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<IDamagable>() == null)
            return;

        collision.GetComponent<IDamagable>().TakeDamage(Damage);

        Destroy(gameObject);
    }
}
