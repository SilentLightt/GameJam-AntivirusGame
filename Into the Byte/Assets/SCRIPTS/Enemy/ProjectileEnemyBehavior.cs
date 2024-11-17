using UnityEngine;

public class ProjectileEnemyBehavior : EnemyBase
{
    public Transform firingPoint;  // Reference to the firing point
    public GameObject projectilePrefab;  // The projectile to instantiate

    public override void Attack()
    {
        if (firingPoint != null && projectilePrefab != null && player != null)
        {
            // Make sure the enemy is facing the player
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            FlipToFacePlayer(directionToPlayer.x);

            // Rotate the firing point to face the player
            RotateFiringPointTowardsPlayer();

            PlayAttackAnimation();

            // Instantiate the projectile at the firing point's position and rotation
            Instantiate(projectilePrefab, firingPoint.position, firingPoint.rotation);
        }
        else
        {
            Debug.LogWarning("Firing point, projectile prefab, or player is not set.");
        }
    }

    private void RotateFiringPointTowardsPlayer()
    {
        if (player != null)
        {
            // Calculate the direction to the player
            Vector3 directionToPlayer = (player.position - firingPoint.position).normalized;

            // Calculate the angle needed to face the player
            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

            // Set the rotation of the firing point to face the player
            firingPoint.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }
    //public void TakeDamage()
    //{
    //    // Specific damage logic here
    //    PlayTakeDamageAnimation();
    //}

}

//using UnityEngine;

//public class ProjectileEnemyBehavior : EnemyBase
//{
//    public GameObject projectilePrefab; // Reference to the projectile prefab

//    public override void Attack()
//    {
//        if (projectilePrefab != null)
//        {
//            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
//            Projectile projectileScript = projectile.GetComponent<Projectile>();
//            if (projectileScript != null && player != null)
//            {
//                projectileScript.Launch(player.position, enemyStats.attackDamage);
//            }
//            Debug.Log("Projectile enemy fires a projectile at the player.");
//        }
//        else
//        {
//            Debug.LogWarning("Projectile prefab not assigned.");
//        }
//    }
//}

