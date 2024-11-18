using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyStats", menuName = "ScriptableObjects/EnemyStats", order = 1)]
public class EnemyStats : ScriptableObject
{
    public float followDistance = 10f;    // Distance within which the enemy starts following the player
    public float attackDistance = 2f;     // Distance within which the enemy starts attacking the player
    public float moveSpeed = 3f;          // Movement speed of the enemy
    public float attackCooldown = 1.5f;   // Time interval between attacks
    public float attackDamage = 10f;      // Damage dealt to the player on attack
    public float health = 100f;
    [Range(0f, 1f)]
    public float coinDropChance = 0.5f;   // Chance to drop a coin (0 = 0%, 1 = 100%)
}
