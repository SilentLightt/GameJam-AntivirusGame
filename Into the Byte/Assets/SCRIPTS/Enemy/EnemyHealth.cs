using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public EnemyBehavior enemyBehavior;   // Reference to the EnemyBehavior script
    public Slider healthSlider;           // UI Slider for enemy health
    private float maxHP;                  // Max health from EnemyStats
    private float currentHP;              // Current health of the enemy
    private float lerpSpeed = 0.05f;      // Speed for easing the slider update
    public Camera playerCamera;          // Reference to the player’s camera
    public Canvas enemyCanvas;           // Canvas for the enemy's health bar
    public RectTransform healthBarRect;  // Health bar rect transform for positioning

    void Start()
    {
        // Find the player's camera on spawn
        FindPlayerCamera();

        // Ensure enemyBehavior is assigned or try to get it from the same GameObject
        if (enemyBehavior == null)
        {
            enemyBehavior = GetComponent<EnemyBehavior>();
        }

        // Initialize health values from EnemyStats
        if (enemyBehavior != null && enemyBehavior.enemyStats != null)
        {
            maxHP = enemyBehavior.enemyStats.health;
            currentHP = maxHP;

            // Initialize slider values
            if (healthSlider != null)
            {
                healthSlider.maxValue = maxHP;
                healthSlider.value = currentHP;
            }
        }

        // Get the canvas component attached to this enemy
        if (enemyCanvas == null)
        {
            enemyCanvas = GetComponentInChildren<Canvas>();
            if (enemyCanvas != null)
            {
                enemyCanvas.worldCamera = playerCamera; // Set the canvas to render in world space and follow the camera
            }
        }

        // Get the rect transform of the health bar
        if (healthSlider != null)
        {
            healthBarRect = healthSlider.GetComponent<RectTransform>();
        }
    }

    void Update()
    {
        // Smoothly update the slider value (LERP)
        if (healthSlider != null && healthSlider.value != currentHP)
        {
            healthSlider.value = Mathf.Lerp(healthSlider.value, currentHP, lerpSpeed);
        }

        // Update the position of the health bar to always follow the enemy
        if (enemyCanvas != null)
        {
            // Set the health bar position above the enemy's position in world space
            Vector3 enemyPosition = transform.position;
            enemyCanvas.transform.position = new Vector3(enemyPosition.x, enemyPosition.y + 2f, enemyPosition.z); // Adjust for height

            // Make the health bar face the camera
            if (playerCamera != null)
            {
                // Adjust rotation to face the camera
                enemyCanvas.transform.rotation = Quaternion.LookRotation(enemyCanvas.transform.position - playerCamera.transform.position);
            }
        }
    }

    private void FindPlayerCamera()
    {
        playerCamera = Camera.main;  // Automatically get the main camera
        if (playerCamera == null)
        {
            Debug.LogWarning("Main Camera not found! Ensure the player camera has the 'MainCamera' tag.");
        }
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            currentHP = 0;
            Die();
        }

        // Update the slider immediately upon taking damage
        if (healthSlider != null)
        {
            healthSlider.value = currentHP;  // Directly set the value here
        }
    }

    private void Die()
    {
        Debug.Log("Enemy died.");
        Destroy(gameObject);  // Destroy enemy on death
    }
}
