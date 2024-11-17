using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;                 // Speed of the projectile
    public float damage;                // Damage dealt by the projectile
    public float projectileLifetime = 5f;  // Lifetime of the projectile
    public bool isGroundProjectile = false; // Flag to determine if the projectile stays on the ground
    public float damageInterval = 1f;   // Time interval between each damage tick (for ground projectiles)

    private float damageTimer = 0f;     // Timer to track intervals for damage over time
    private bool playerInTrigger = false; // Check if the player is in trigger for ground projectile

    public void Launch(Vector3 targetPosition, float damageAmount)
    {
        damage = damageAmount;
        Vector3 direction = (targetPosition - transform.position).normalized;
        GetComponent<Rigidbody2D>().velocity = direction * speed;
    }

    private void Update()
    {
        // Update lifetime for normal projectiles
        if (!isGroundProjectile)
        {
            Destroy(gameObject, projectileLifetime);
        }

        // Handle damage over time if player is in the trigger zone for ground projectiles
        if (playerInTrigger && isGroundProjectile)
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= damageInterval)
            {
                // Deal periodic damage to the player
                HealthBar.instance.PlayerTakeDamage(damage);
                damageTimer = 0f;  // Reset the timer
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!isGroundProjectile)
            {
                // Deal instant damage for normal projectiles
                HealthBar.instance.PlayerTakeDamage(damage);
                Destroy(gameObject);
            }
        }
        else if (collision.CompareTag("Ground") && isGroundProjectile)
        {
            // Ground projectiles stay on the ground
            GetComponent<Rigidbody2D>().velocity = Vector2.zero; // Stop movement
            Destroy(gameObject, projectileLifetime); // Destroy after lifetime expires
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isGroundProjectile)
        {
            // Player is staying in the ground projectile's trigger zone
            playerInTrigger = true;
            Destroy(gameObject,projectileLifetime);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isGroundProjectile)
        {
            // Player left the trigger zone
            playerInTrigger = false;
            damageTimer = 0f; // Reset damage timer when player exits
        }
    }
}

//using UnityEngine;

//public class Projectile : MonoBehaviour
//{
//    public float speed;
//    public float damage;
//    public float projectilelifetime;
//    public void Launch(Vector3 targetPosition, float damageAmount)
//    {
//        damage = damageAmount;
//        Vector3 direction = (targetPosition - transform.position).normalized;
//        GetComponent<Rigidbody2D>().velocity = direction * speed;
//    }

//    //private void OnCollisionStay2D(Collision2D collision)
//    //{
//    //    if (gameObject.CompareTag("Player"))
//    //    {
//    //        HealthBar.instance.PlayerTakeDamage(damage);
//    //        Destroy(gameObject,projectilelifetime);
//    //    }
//    //}
//    private void OnTriggerStay2D(Collider2D collision)
//    {
//        if (collision.CompareTag("Player"))
//        {
//            HealthBar.instance.PlayerTakeDamage(damage);
//            Destroy(gameObject,projectilelifetime);
//        }
//    }
//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (collision.CompareTag("Player"))
//        {
//            HealthBar.instance.PlayerTakeDamage(damage);
//           Destroy(gameObject);
//        }
//        //else if (collision.CompareTag("Obstacle")) // Optional: destroy projectile on obstacle collision
//        //{
//        //    Destroy(gameObject);
//        //}
//        else if (collision.CompareTag("Ground")) // Optional: destroy projectile on obstacle collision
//        {
//            Destroy(gameObject, projectilelifetime);
//        }
//    }
//}

