using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PredirectedProjectile : Projectile
{
    [SerializeField] private GameObject explosionPrefab;
    public Vector2 Direction { get; set; }
    public float Range { get; set; }
    private Rigidbody2D _rigidbody;

    public void Initialize(Vector2 direction, float travelSpeed, int damage, float range)
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        Damage = damage;
        Range = range;
        Emmit(direction, travelSpeed);
    }

    public void Emmit(Vector2 direction, float travelSpeed)
    {
        _rigidbody.AddForce(direction.normalized * travelSpeed, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<IDamagable>() == null)
            return;

        collision.GetComponent<IDamagable>().TakeDamage(Damage);
        if(explosionPrefab)
        {
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            var particleSystem = explosion.GetComponent<ParticleSystem>();
            var main = particleSystem.main;
            main.startSizeMultiplier = Damage/2f;
            foreach (Transform child in explosion.transform)
            {
                if (child.GetComponent<ParticleSystem>() != null)
                {
                    particleSystem = child.GetComponent<ParticleSystem>();
                    main = particleSystem.main;
                    main.startSizeMultiplier = Damage/3f;
                }
            }
                
            explosion.GetComponent<Explosion>().Detonate(Damage / 2f, (int)(Damage / 2f));
        }

        Destroy(gameObject);
    }
}
