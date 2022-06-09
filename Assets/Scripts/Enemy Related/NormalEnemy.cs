using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : Enemy, IDamagable
{
    public void TakeDamage(int damage)
    {
        LooseHealth(damage);
    }
}
