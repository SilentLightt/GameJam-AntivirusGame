using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public EnemyBehavior enemyBehavior;   // Reference to the EnemyBehavior script
    public float damageAmount = 10f;      // Amount of damage to deal to the enemy

    private void Start()
    {
        // Ensure enemyBehavior is assigned, or try to get it from the same GameObject
        if (enemyBehavior == null)
        {
            enemyBehavior = GetComponent<EnemyBehavior>();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Detect if the collision is with a player attack (e.g., bullet or melee)
        if (collision.gameObject.CompareTag("PlayerAttack"))
        {
            // Call the TakeDamage method on EnemyBehavior
            if (enemyBehavior != null)
            {
                enemyBehavior.TakeDamage(damageAmount);
                Debug.Log("Enemy took " + damageAmount + " damage from player's attack.");
            }
            else
            {
                Debug.LogWarning("EnemyBehavior component not found on the enemy.");
            }

            // Optionally, destroy the player's attack object after it hits the enemy
            Destroy(collision.gameObject);
        }
    }
}

