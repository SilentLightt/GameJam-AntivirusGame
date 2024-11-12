using UnityEngine;

public class ProjectileEnemyBehavior : EnemyBase
{
    public GameObject projectilePrefab; // Reference to the projectile prefab

    public override void Attack()
    {
        if (projectilePrefab != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Projectile projectileScript = projectile.GetComponent<Projectile>();
            if (projectileScript != null && player != null)
            {
                projectileScript.Launch(player.position, enemyStats.attackDamage);
            }
            Debug.Log("Projectile enemy fires a projectile at the player.");
        }
        else
        {
            Debug.LogWarning("Projectile prefab not assigned.");
        }
    }
}

