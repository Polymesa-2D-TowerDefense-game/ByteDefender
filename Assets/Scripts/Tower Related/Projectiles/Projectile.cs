using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int Damage { get; set; }
    public float TravelSpeed { get; set; }

    public bool CanSeeCryptedEnemies { get; set; }
}
