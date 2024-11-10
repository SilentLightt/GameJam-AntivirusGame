using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public float speed = 10f;                // Speed of the projectile
    public float lifetime = 2f;              // How long the projectile exists before being destroyed
    public float damage = 10f;               // Damage dealt by the projectile
    public Vector2 direction = Vector2.right; // Default direction of the projectile (can be modified when instantiated)

    void Start()
    {
        // Destroy the projectile after the specified lifetime to prevent memory buildup
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Move the projectile in the specified direction
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the projectile hits an enemy with the EnemyHealth component
        EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            // Call the TakeDamage method on the enemy and pass the projectile's damage
            enemyHealth.enemyBehavior.TakeDamage(damage);
            Debug.Log("Projectile hit enemy for " + damage + " damage.");
        }

        // Destroy the projectile on collision
        Destroy(gameObject);
    }
}

