using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public EnemyStats enemyStats;      // Reference to the ScriptableObject for stats
    public Slider healthSlider;        // Slider for instant health display
    public Slider easeHealthSlider;    // Slider for smooth health decrease display

    private float currentHealth;       // Current health of the enemy
    private float lerpSpeed = 0.05f;   // Speed at which easeHealthSlider interpolates
    private PlayerProjectile playerProjectile;
    void Start()
    {
        if (enemyStats != null)
        {
            currentHealth = enemyStats.health;
            healthSlider.maxValue = enemyStats.health;
            healthSlider.value = currentHealth;
            easeHealthSlider.maxValue = enemyStats.health;
            easeHealthSlider.value = currentHealth;
        }
        else
        {
            Debug.LogError("EnemyStats not assigned. Please assign an EnemyStats ScriptableObject.");
        }
        if (playerProjectile == null)
        {
            playerProjectile = FindObjectOfType<PlayerProjectile>();
            if (playerProjectile == null)
            {
                Debug.LogError("PlayerProjectile not found in the scene.");
            }
        }
    }

    void Update()
    {
        // Smoothly interpolate easeHealthSlider value towards currentHealth
        if (easeHealthSlider.value > currentHealth)
        {
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, currentHealth, lerpSpeed);
        }
    }

    public void EnemyTakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemy takes " + damage + " damage. Current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }

        healthSlider.value = currentHealth;  // Update healthSlider instantly
        //currentHealth -= playerProjectile.damage;
        //Debug.Log("Enemy takes " + playerProjectile.damage + " damage. Current health: " + currentHealth);

        //if (currentHealth <= 0)
        //{
        //    currentHealth = 0;
        //    Die();
        //}

        //healthSlider.value = enemyStats.health;  // Update healthSlider instantly
    }

    void Die()
    {
        Debug.Log("Enemy died.");
        Destroy(gameObject);
        // Optionally, trigger death animations or other effects here
    }
}
