using UnityEngine;

public class PlayerMeleeAttack : PlayerAttackBase
{
    public float Sworddamage = 20f;               // The damage dealt by the melee attack
    public float attackRange = 1f;           // Range of the melee attack
    public LayerMask enemyLayer;             // Layer mask to identify enemies
    public Transform attackPoint;            // The point from where the melee attack is performed
    public Animator animator;
    public PlayerController controller;

    private void Start()
    {
        Animator animator = GetComponentInParent<Animator>();
        PlayerController controller = GetComponentInParent<PlayerController>();

        // Initialize your components, if necessary
    }
    public void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Attack(); // Trigger melee attack
            animator.SetTrigger("MeleeAttack");

        }
    }

    public override void Attack()
    {
        // Detect enemies within attack range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        // Loop through all enemies in range and apply damage
        foreach (Collider2D enemy in hitEnemies)
        {
            // Assuming the enemy has a health system with a `TakeDamage` method
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.EnemyTakeDamage(Sworddamage);
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

//using UnityEngine;

//public class PlayerMeleeAttack : PlayerAttackBase
//{
//    public Transform attackPoint;
//    public float attackRange = 1f;
//    public float damage = 15f;
//    public LayerMask enemyLayer;

//    public override void Attack()
//    {
//        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

//        foreach (Collider2D enemy in hitEnemies)
//        {
//            enemy.GetComponent<EnemyBase>()?.TakeDamage(damage);
//        }
//    }

//    private void OnDrawGizmosSelected()
//    {
//        if (attackPoint == null) return;
//        Gizmos.color = Color.red;
//        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
//    }
//}
