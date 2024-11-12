using UnityEngine;

public class MeleeEnemyBehavior : EnemyBase
{
    public override void Attack()
    {
        if (HealthBar.instance != null)
        {
            HealthBar.instance.PlayerTakeDamage(enemyStats.attackDamage);
            Debug.Log("Melee enemy attacks player for " + enemyStats.attackDamage + " damage.");
        }
    }
}

