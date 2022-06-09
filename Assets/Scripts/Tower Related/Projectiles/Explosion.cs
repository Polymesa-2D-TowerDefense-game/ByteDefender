using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] LayerMask enemiesLayer;

    public void Detonate(float range, int damage)
    {
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, range, enemiesLayer);
        foreach (var enemy in enemiesInRange)
        {
            if (enemy.GetComponent<IDamagable>() != null)
                enemy.GetComponent<IDamagable>().TakeDamage(damage);
        }
        //Destroy(gameObject);
    }
}
