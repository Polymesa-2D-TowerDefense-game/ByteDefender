using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathObstacle : Projectile
{
    public Vector2 TargetPosition { get; set; }


    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, TargetPosition) >= 0.1f)
            transform.position = Vector2.MoveTowards(transform.position, TargetPosition, TravelSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<IDamagable>() == null)
            return;

        collision.GetComponent<IDamagable>().TakeDamage(1);
        Damage--;

        if(Damage >= 0)
            Destroy(gameObject);
    }
}
