//using UnityEngine;
//using UnityEngine.UI;

//public class EnemyHealth : MonoBehaviour
//{
//    public EnemyStats enemyStats;      // Reference to the ScriptableObject for stats
//    public Slider healthSlider;        // Slider for instant health display
//    public Slider easeHealthSlider;    // Slider for smooth health decrease display
//    public Canvas enemyCanvas;         // Reference to the Canvas for enemy health bar

//    private float currentHealth;       // Current health of the enemy
//    private float lerpSpeed = 0.05f;   // Speed at which easeHealthSlider interpolates
//    private PlayerProjectile playerProjectile;
//    void Start()
//    {
//        if (enemyStats != null)
//        {
//            currentHealth = enemyStats.health;
//            healthSlider.maxValue = enemyStats.health;
//            healthSlider.value = currentHealth;
//            easeHealthSlider.maxValue = enemyStats.health;
//            easeHealthSlider.value = currentHealth;
//        }
//        else
//        {
//            Debug.LogError("EnemyStats not assigned. Please assign an EnemyStats ScriptableObject.");
//        }

//        if (playerProjectile == null)
//        {
//            playerProjectile = FindObjectOfType<PlayerProjectile>();
//            if (playerProjectile == null)
//            {
//               // Debug.LogError("PlayerProjectile not found in the scene.");
//            }
//        }

//        // Automatically get the Main Camera and attach it to the Canvas (world space)
//        if (enemyCanvas == null)
//        {
//            enemyCanvas = GetComponentInChildren<Canvas>();  // Automatically find the Canvas in the enemy prefab
//        }

//        if (enemyCanvas != null)
//        {
//            Camera mainCamera = Camera.main;
//            if (mainCamera != null)
//            {
//                enemyCanvas.worldCamera = mainCamera; // Attach the main camera to the World Space Canvas
//            }
//            else
//            {
//                Debug.LogError("Main Camera not found in the scene.");
//            }
//        }
//        else
//        {
//            Debug.LogError("Enemy Canvas not assigned and could not be found in the prefab.");
//        }
//    }

//    void Update()
//    {
//        // Smoothly interpolate easeHealthSlider value towards currentHealth
//        if (easeHealthSlider.value > currentHealth)
//        {
//            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, currentHealth, lerpSpeed);
//        }
//    }

//    public void EnemyTakeDamage(float damage)
//    {
//        currentHealth -= damage;
//       // Debug.Log("Enemy takes " + damage + " damage. Current health: " + currentHealth);

//        if (currentHealth <= 0)
//        {
//            currentHealth = 0;
//            Die();
//        }

//        healthSlider.value = currentHealth;  
//    }

//    void Die()
//    {
//        Debug.Log("Enemy died.");
//        Destroy(gameObject);
//        // Optionally, trigger death animations or other effects here
//    }
//}
using UnityEngine;
using UnityEngine.UI;
public class EnemyHealth : MonoBehaviour
{
    public Slider healthSlider;
    public Slider easeHealthSlider;
    public Canvas enemyCanvas;
    private float lerpSpeed = 2f;
    private float targetHealth; // Tracks the desired health for the ease slider

    void Start()
    {
        var enemyBase = GetComponent<EnemyBase>();
        if (enemyBase != null)
        {
            enemyBase.OnHealthChanged += UpdateHealthUI;
        }
        // Automatically get the Main Camera and attach it to the Canvas (world space)
        if (enemyCanvas == null)
        {
            enemyCanvas = GetComponentInChildren<Canvas>();  // Automatically find the Canvas in the enemy prefab
        }

        if (enemyCanvas != null)
        {
            Camera mainCamera = Camera.main;
            if (mainCamera != null)
            {
                enemyCanvas.worldCamera = mainCamera; // Attach the main camera to the World Space Canvas
            }
            else
            {
                Debug.LogError("Main Camera not found in the scene.");
            }
        }
        else
        {
            Debug.LogError("Enemy Canvas not assigned and could not be found in the prefab.");
        }
        //    }
    }

    void Update()
    {
        // Smoothly interpolate easeHealthSlider toward targetHealth
        if (easeHealthSlider.value != targetHealth)
        {
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, targetHealth, Time.deltaTime * lerpSpeed);
        }
    }

    void UpdateHealthUI(float currentHealth)
    {
        healthSlider.value = currentHealth; // Instant update for the direct health slider
        targetHealth = currentHealth;      // Set the target for the ease slider
    }
}
