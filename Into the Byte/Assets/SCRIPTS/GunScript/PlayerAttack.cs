using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject projectilePrefab;    // Reference to the projectile prefab
    public Transform firePoint;            // Point from where the projectile will be fired

    void Update()
    {
        // Check for attack input (e.g., spacebar or mouse click)
        if (Input.GetButtonDown("Fire1")) // "Fire1" is the default input for the left mouse button or Ctrl
        {
            FireProjectile();
        }
    }

    void FireProjectile()
    {
        // Instantiate the projectile at the fire point position
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Set the direction of the projectile
        PlayerProjectile projectileScript = projectile.GetComponent<PlayerProjectile>();
        if (projectileScript != null)
        {
            // Set the direction based on player orientation (example: right by default)
            projectileScript.direction = Vector2.right; // Or use the player's facing direction if applicable
        }
    }
}

