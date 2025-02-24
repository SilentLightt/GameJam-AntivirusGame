using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public EnemyStats enemyStats;                // Reference to the ScriptableObject containing the enemy's stats
    private Transform player;                    // Reference to the player
    private float currentHealth;                 // Current health of the enemy
    private float lastAttackTime = 0f;           // Timer to track the time of the last attack
    private EnemyHealth enemyHealth;
    public EnemyAnimations enemyAnimations;     // Reference to EnemyAnimations for controlling animations

    void Start()
    {
        // Automatically find the player if not set in the Inspector
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player not found! Please assign the player object in the Inspector or ensure it has the 'Player' tag.");
        }

        // Initialize enemy health from the ScriptableObject
        enemyHealth = GetComponent<EnemyHealth>();  // Get the EnemyHealth component
        if (enemyStats != null)
        {
            currentHealth = enemyStats.health;
        }

        enemyAnimations = GetComponent<EnemyAnimations>();

        // If not found on the parent, look for it in children
        if (enemyAnimations == null)
        {
            enemyAnimations = GetComponentInChildren<EnemyAnimations>();
        }

        if (enemyAnimations == null)
        {
            Debug.LogError("EnemyAnimations component not found on parent or children! Please attach the EnemyAnimations script.");
        }
    }

    void Update()
    {
        if (player == null || enemyStats == null) return; // Exit if the player or enemy stats reference is missing

        // Calculate the distance to the player
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Check if within follow distance but not within attack range
        if (distanceToPlayer <= enemyStats.followDistance && distanceToPlayer > enemyStats.attackDistance)
        {
            FollowPlayer();
        }
        // Check if within attack range
        else if (distanceToPlayer <= enemyStats.attackDistance)
        {
            AttackPlayer();
        }
    }

    void FollowPlayer()
    {
        // Move towards the player
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * enemyStats.moveSpeed * Time.deltaTime;

        // Play walk animation
        enemyAnimations.PlayWalkAnimation();

        // Flip the enemy to face the player
        if (direction.x > 0 && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        else if (direction.x < 0 && transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    void AttackPlayer()
    {
        // Check if enough time has passed since the last attack
        if (Time.time >= lastAttackTime + enemyStats.attackCooldown)
        {
            lastAttackTime = Time.time;

            // Play attack animation
            enemyAnimations.PlayAttackAnimation();

            // Deal damage to the player
            if (HealthBar.instance != null)
            {
                HealthBar.instance.PlayerTakeDamage(enemyStats.attackDamage);
                Debug.Log("Enemy attacks the player for " + enemyStats.attackDamage + " damage!");
            }
        }
    }

    void TakeDamage()
    {
        // Play take damage animation
        enemyAnimations.PlayTakeDamageAnimation();

        // Reduce health
        currentHealth -= enemyStats.attackDamage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Play death animation
        enemyAnimations.PlayDeathAnimation();

        // Perform any death-related actions here (e.g., drop loot)
        Debug.Log("Enemy died.");
        Destroy(gameObject);
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, enemyStats.followDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemyStats.attackDistance);
    }
}

//originalscript
//public class EnemyBehavior : MonoBehaviour
//{
//    public EnemyStats enemyStats;                // Reference to the ScriptableObject containing the enemy's stats
//    private Transform player;                    // Reference to the player
//    private float currentHealth;                 // Current health of the enemy
//    private float lastAttackTime = 0f;           // Timer to track the time of the last attack

//    void Start()
//    {
//        // Automatically find the player if not set in the Inspector
//        GameObject playerObject = GameObject.FindWithTag("Player");
//        if (playerObject != null)
//        {
//            player = playerObject.transform;
//        }
//        else
//        {
//            Debug.LogError("Player not found! Please assign the player object in the Inspector or ensure it has the 'Player' tag.");
//        }

//        // Initialize enemy health from the ScriptableObject
//        if (enemyStats != null)
//        {
//            currentHealth = enemyStats.health;
//        }
//    }

//    void Update()
//    {
//        if (player == null || enemyStats == null) return; // Exit if the player or enemy stats reference is missing

//        // Calculate the distance to the player
//        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

//        // Check if within follow distance but not within attack range
//        if (distanceToPlayer <= enemyStats.followDistance && distanceToPlayer > enemyStats.attackDistance)
//        {
//            FollowPlayer();
//        }
//        // Check if within attack range
//        else if (distanceToPlayer <= enemyStats.attackDistance)
//        {
//            AttackPlayer();
//        }
//    }

//    void FollowPlayer()
//    {
//        // Move towards the player
//        Vector3 direction = (player.position - transform.position).normalized;
//        transform.position += direction * enemyStats.moveSpeed * Time.deltaTime;

//        // Optionally, flip the enemy to face the player (for 2D sprites)
//        if (direction.x > 0 && transform.localScale.x < 0)
//        {
//            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
//        }
//        else if (direction.x < 0 && transform.localScale.x > 0)
//        {
//            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
//        }
//    }

//    void AttackPlayer()
//    {
//        // Check if enough time has passed since the last attack
//        if (Time.time >= lastAttackTime + enemyStats.attackCooldown)
//        {
//            lastAttackTime = Time.time;

//            // Perform attack (this could be damaging the player, playing an animation, etc.)
//            Debug.Log("Enemy attacks the player for " + enemyStats.attackDamage + " damage!");

//            // If there's a PlayerHealth script or similar, you could call a damage function here:
//            // player.GetComponent<PlayerHealth>().TakeDamage(enemyStats.attackDamage);
//        }
//    }

//    // Method to take damage
//    public void TakeDamage(float damage)
//    {
//        currentHealth -= damage;
//        Debug.Log("Enemy took " + damage + " damage. Current health: " + currentHealth);

//        if (currentHealth <= 0)
//        {
//            Die();
//        }
//    }

//    void Die()
//    {
//        // Perform any death-related actions here (e.g., play an animation, drop loot)
//        Debug.Log("Enemy died.");
//        Destroy(gameObject);
//    }
//    public void OnDrawGizmosSelected()
//    {
//        Gizmos.color = Color.yellow;
//        Gizmos.DrawWireSphere(transform.position, enemyStats.followDistance);
//        Gizmos.color = Color.red;
//        Gizmos.DrawWireSphere(transform.position, enemyStats.attackDistance);

//    }
//}




