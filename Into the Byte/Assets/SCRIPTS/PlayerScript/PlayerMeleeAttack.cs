using UnityEngine;
using System.Collections;
//public class PlayerMeleeAttack : PlayerAttackBase
//{
//    public float Sworddamage = 20f;               // The damage dealt by the melee attack
//    public float attackRange = 1f;           // Range of the melee attack
//    public LayerMask enemyLayer;             // Layer mask to identify enemies
//    public Transform attackPoint;            // The point from where the melee attack is performed
//    public Animator animator;
//    public PlayerController controller;

//    private void Start()
//    {
//        Animator animator = GetComponentInParent<Animator>();
//        PlayerController controller = GetComponentInParent<PlayerController>();

//        // Initialize your components, if necessary
//    }
//    public void Update()
//    {
//        if (Input.GetButtonDown("Fire1"))
//        {
//            Attack(); // Trigger melee attack
//            animator.SetTrigger("MeleeAttack");

//        }
//    }

//    public override void Attack()
//    {
//        // Detect enemies within attack range
//        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

//        // Loop through all enemies in range and apply damage
//        foreach (Collider2D enemy in hitEnemies)
//        {
//            // Assuming the enemy has a health system with a `TakeDamage` method
//            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
//            if (enemyHealth != null)
//            {
//                enemyHealth.EnemyTakeDamage(Sworddamage);
//                Debug.Log("Melee attack hit enemy for " + Sworddamage + " damage.");
//            }
//        }
//    }

//    private void OnDrawGizmosSelected()
//    {
//        // This is just for visualization of the attack range in the Unity Editor
//        if (attackPoint == null)
//            return;

//        Gizmos.color = Color.red;
//        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
//    }
//}

public class PlayerMeleeAttack : PlayerAttackBase
{
    public float Sworddamage = 20f;               // The damage dealt by the melee attack
    public float attackRange = 1f;                // Range of the melee attack
    public LayerMask enemyLayer;                  // Layer mask to identify enemies
    public Transform attackPoint;                 // The point from where the melee attack is performed
    public Animator animator;
    public PlayerController controller;

    public float attackCooldown = 0.5f;           // Cooldown between attacks (in seconds)
    private float lastAttackTime = -Mathf.Infinity; // Track the time of the last attack

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
        controller = GetComponentInParent<PlayerController>();

        // Initialize your components, if necessary
    }

    public void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= lastAttackTime + attackCooldown)
        {
            // If the cooldown has passed, trigger the attack
            lastAttackTime = Time.time; // Update the time of the last attack
            StartCoroutine(AttackWithDelay()); // Start the attack coroutine with delay
        }
    }

    private IEnumerator AttackWithDelay()
    {
        // Trigger the melee attack animation
        animator.SetTrigger("MeleeAttack");

        // Perform the attack
        Attack();

        // Optional: Add a small delay for visual consistency
        yield return new WaitForSeconds(0.1f); // Small delay before next possible attack (optional)
    }

    public override void Attack()
    {
        // Detect enemies within attack range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        // Loop through all enemies in range and apply damage
        foreach (Collider2D enemy in hitEnemies)
        {
            // Assuming the enemy has a health system with a `TakeDamage` method
            EnemyBase enemyHealth = enemy.GetComponent<EnemyBase>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(Sworddamage);
                Debug.Log("Melee attack hit enemy for " + Sworddamage + " damage.");
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // This is just for visualization of the attack range in the Unity Editor
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
