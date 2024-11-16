using UnityEngine;

public class PlayerMeleeAttack : PlayerAttackBase
{
    public Transform attackPoint;
    public float attackRange = 1f;
    public float damage = 15f;
    public LayerMask enemyLayer;

    public override void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyBase>()?.TakeDamage(damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
