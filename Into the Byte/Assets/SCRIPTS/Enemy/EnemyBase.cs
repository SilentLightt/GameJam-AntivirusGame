using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    public EnemyStats enemyStats; // Reference to the ScriptableObject with enemy stats
    protected Transform player;
    protected float currentHealth;
    protected float lastAttackTime = 0f;

    protected virtual void Start()
    {
        // Find the player automatically
        player = GameObject.FindWithTag("Player")?.transform;
        if (player == null)
        {
            Debug.LogError("Player not found! Make sure there's an object tagged 'Player'.");
        }

        // Initialize health
        currentHealth = enemyStats.health;
    }

    protected virtual void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Shared movement and distance check logic
        if (distanceToPlayer <= enemyStats.followDistance && distanceToPlayer > enemyStats.attackDistance)
        {
            FollowPlayer();
        }
        else if (distanceToPlayer <= enemyStats.attackDistance && Time.time >= lastAttackTime + enemyStats.attackCooldown)
        {
            Attack();
            lastAttackTime = Time.time;
        }
    }

    protected void FollowPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * enemyStats.moveSpeed * Time.deltaTime;
        FlipToFacePlayer(direction.x);
    }

    protected void FlipToFacePlayer(float directionX)
    {
        if (directionX > 0 && transform.localScale.x < 0 || directionX < 0 && transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    public abstract void Attack(); // Abstract method for enemy-specific attacks

    public virtual void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0) Die();
    }

    protected virtual void Die()
    {
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

