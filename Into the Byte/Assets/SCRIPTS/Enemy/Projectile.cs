using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 100f;
    private float damage;

    public void Launch(Vector3 targetPosition, float damageAmount)
    {
        damage = damageAmount;
        Vector3 direction = (targetPosition - transform.position).normalized;
        GetComponent<Rigidbody2D>().velocity = direction * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            HealthBar.instance.PlayerTakeDamage(damage);
            Destroy(gameObject);
        }
        //else if (collision.CompareTag("Obstacle")) // Optional: destroy projectile on obstacle collision
        //{
        //    Destroy(gameObject);
        //}
        else if (collision.CompareTag("Ground")) // Optional: destroy projectile on obstacle collision
        {
            Destroy(gameObject);
        }
    }
}

