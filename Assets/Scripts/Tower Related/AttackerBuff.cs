using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerBuff : MonoBehaviour
{
    [SerializeField]
    public bool canSeeCrypted = false;
    public float BuffDuration { get; set; }
    public int DamageBuff { get; set; }
    public float AttackSpeedBuff { get; set; } 
    public float RangeBuff { get; set; }
    Attacker TowerToBuff { get; set; }

    public void Initialize(Attacker towerToBuff, float buffDuration, int damageBuff, float attackSpeedBuff, float rangeBuff)
    {
        TowerToBuff = towerToBuff;
        BuffDuration = buffDuration;
        DamageBuff = damageBuff;
        AttackSpeedBuff = attackSpeedBuff;
        RangeBuff = rangeBuff;

        Buff();
    }

    private void Buff()
    {
        TowerToBuff.Damage += DamageBuff;
        TowerToBuff.AttackSpeed += AttackSpeedBuff;
        TowerToBuff.Range += RangeBuff;
        TowerToBuff.CanSeeCryptedEnemies = canSeeCrypted;
        TowerToBuff.UpdateRangeIndicator();
        StartCoroutine(Debuff());
    }

    IEnumerator Debuff()
    {
        yield return new WaitForSeconds(BuffDuration);
        TowerToBuff.Damage -= DamageBuff;
        TowerToBuff.AttackSpeed -= AttackSpeedBuff;
        TowerToBuff.Range -= RangeBuff;
        TowerToBuff.CanSeeCryptedEnemies = false;
        TowerToBuff.UpdateRangeIndicator();
        Destroy(gameObject);
    }
}


