using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CryptedEnemy : Enemy, IDamagable
{
    public void TakeDamage(int damage)
    {
        LooseHealth(damage);
    }
}
