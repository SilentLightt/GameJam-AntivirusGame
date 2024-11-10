using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public Transform player;                   // Reference to the player
    public float followDistance = 10f;         // Distance within which the enemy starts following the player
    public float attackDistance = 2f;          // Distance within which the enemy starts attacking the player
    public float moveSpeed = 3f;               // Movement speed of the enemy
    public float attackCooldown = 1.5f;        // Time interval between attacks

    private float lastAttackTime = 0f;         // Timer to track the time of the last attack
    void Start()
    {
        // Automatically find the player if not set in the Inspector
        if (player == null)
        {
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
            else
            {
                Debug.LogError("Player not found! Please assign the player object in the Inspector or ensure it has the 'Player' tag.");
            }
        }
    }
    void Update()
    {
        // Calculate the distance to the player
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Check if within follow distance but not within attack range
        if (distanceToPlayer <= followDistance && distanceToPlayer > attackDistance)
        {
            FollowPlayer();
        }
        // Check if within attack range
        else if (distanceToPlayer <= attackDistance)
        {
            AttackPlayer();
        }
    }

    void FollowPlayer()
    {
        // Move towards the player
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;

        // Optionally, flip the enemy to face the player (for 2D sprites)
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
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;

            // Perform attack (this could be damaging the player, playing an animation, etc.)
            Debug.Log("Enemy attacks the player!");

            // If there's a PlayerHealth script or similar, you could call a damage function here:
            // player.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
        }
    }
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, followDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);

    }
}

