using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject projectilePrefab;    // Reference to the projectile prefab
    public Transform firePoint;            // Point from where the projectile will be fired

    public AmmoManager ammoManager;        // Reference to the AmmoManager script

    void Update()
    {
        // Check for attack input (e.g., spacebar or mouse click)
        if (Input.GetButtonDown("Fire1"))
        {
            FireProjectile();
        }

        // Example of reloading (can be adjusted to your game logic)
        if (Input.GetKeyDown(KeyCode.R)) // Press R to reload
        {
            ammoManager.ReloadAmmo();
        }

        RotateFirePoint();
    }

    void FireProjectile()
    {
        if (ammoManager.TryFireAmmo())
        {
            // Get the direction from the fire point to the mouse position
            Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - firePoint.position).normalized;

            // Instantiate the projectile
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

            // Set the direction of the projectile
            PlayerProjectile projectileScript = projectile.GetComponent<PlayerProjectile>();
            if (projectileScript != null)
            {
                projectileScript.direction = direction;
            }
        }
    }

    void RotateFirePoint()
    {
        // Get the direction from the fire point to the mouse position
        Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - firePoint.position).normalized;

        // Calculate the angle to rotate
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Apply the rotation to the firePoint
        firePoint.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}



//public class PlayerAttack : MonoBehaviour
//{
//    public GameObject projectilePrefab;    // Reference to the projectile prefab
//    public Transform firePoint;            // Point from where the projectile will be fired

//    public int maxAmmo = 10;              // Maximum ammo the player can have at a time
//    public int currentAmmo;               // Current ammo count the player has
//    public int reserveAmmo = 100;         // Total reserve ammo available

//    // Reference to the TextMeshPro UI elements
//    public TextMeshProUGUI currentAmmoText;  // Reference to display current ammo
//    public TextMeshProUGUI reserveAmmoText;  // Reference to display reserve ammo

//    void Start()
//    {
//        // Initialize the current ammo to the max ammo at the start
//        currentAmmo = maxAmmo;
//        UpdateAmmoUI(); // Update UI at the start
//    }

//    void Update()
//    {
//        // Check for attack input (e.g., spacebar or mouse click) and ensure the player has ammo
//        if (Input.GetButtonDown("Fire1") && currentAmmo > 0)
//        {
//            FireProjectile();
//        }

//        // Example of reloading (can be adjusted to your game logic)
//        if (Input.GetKeyDown(KeyCode.R)) // Press R to reload
//        {
//            ReloadAmmo();
//        }
//        RotateFirePoint();
//    }

//    void FireProjectile()
//    {
//        // Get the direction from the fire point to the mouse position (or your desired direction)
//        Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - firePoint.position).normalized;

//        // Instantiate the projectile at the fire point position (ensure it's updated with the player's movement)
//        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

//        // Set the direction of the projectile
//        PlayerProjectile projectileScript = projectile.GetComponent<PlayerProjectile>();
//        if (projectileScript != null)
//        {
//            // Set the direction based on the mouse position or input
//            projectileScript.direction = direction;
//        }

//        // Reduce ammo count after firing
//        currentAmmo--;
//        UpdateAmmoUI(); // Update the ammo UI after firing
//    }
//    void RotateFirePoint()
//    {
//        // Get the direction from the fire point to the mouse position
//        Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - firePoint.position).normalized;

//        // Calculate the angle to rotate
//        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

//        // Apply the rotation to the firePoint
//        firePoint.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
//    }
//    void ReloadAmmo()
//    {
//        // Check if there's enough reserve ammo to refill max ammo
//        if (reserveAmmo > 0)
//        {
//            // Calculate how much ammo is needed to reload max ammo
//            int ammoNeeded = maxAmmo - currentAmmo;

//            // If there's enough reserve ammo to completely refill max ammo
//            if (reserveAmmo >= ammoNeeded)
//            {
//                reserveAmmo -= ammoNeeded;  // Decrease reserve ammo
//                currentAmmo = maxAmmo;      // Refill max ammo
//            }
//            else
//            {
//                currentAmmo += reserveAmmo; // Refill as much as possible with reserve ammo
//                reserveAmmo = 0;            // Deplete reserve ammo
//            }

//            UpdateAmmoUI(); // Update the ammo UI after reloading
//        }
//    }

//    // Update the ammo UI (text fields) based on the current ammo and reserve ammo
//    void UpdateAmmoUI()
//    {
//        if (currentAmmoText != null)
//        {
//            currentAmmoText.text = "Ammo: " + currentAmmo.ToString(); // Update current ammo display
//        }

//        if (reserveAmmoText != null)
//        {
//            reserveAmmoText.text = "Reserve: " + reserveAmmo.ToString(); // Update reserve ammo display
//        }
//    }
//}


//working with reload and limited bullets
//public class PlayerAttack : MonoBehaviour
//{
//    public GameObject projectilePrefab;    // Reference to the projectile prefab
//    public Transform firePoint;            // Point from where the projectile will be fired
//    public bool isFacingRight = true;      // Track player's facing direction (true for right, false for left)

//    public int maxAmmo = 10;              // Maximum ammo the player can have at a time
//    public int currentAmmo;              // Current ammo count the player has
//    public int reserveAmmo = 100;         // Total reserve ammo available

//    // Reference to the TextMeshPro UI elements
//    public TextMeshProUGUI currentAmmoText;  // Reference to display current ammo
//    public TextMeshProUGUI reserveAmmoText;  // Reference to display reserve ammo

//    void Start()
//    {
//        // Initialize the current ammo to the max ammo at the start
//        currentAmmo = maxAmmo;
//        UpdateAmmoUI(); // Update UI at the start
//    }

//    void Update()
//    {
//        // Check for attack input (e.g., spacebar or mouse click) and ensure the player has ammo
//        if (Input.GetButtonDown("Fire1") && currentAmmo > 0)
//        {
//            FireProjectile();
//        }

//        // Example of switching the facing direction (You can implement this based on your input or player movement)
//        if (Input.GetKeyDown(KeyCode.A)) // Example: Press A to face left
//        {
//            isFacingRight = false;
//        }
//        else if (Input.GetKeyDown(KeyCode.D)) // Example: Press D to face right
//        {
//            isFacingRight = true;
//        }

//        // Example of reloading (can be adjusted to your game logic)
//        if (Input.GetKeyDown(KeyCode.R)) // Press R to reload
//        {
//            ReloadAmmo();
//        }
//    }

//    void FireProjectile()
//    {
//        // Instantiate the projectile at the fire point position
//        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

//        // Set the direction of the projectile
//        PlayerProjectile projectileScript = projectile.GetComponent<PlayerProjectile>();
//        if (projectileScript != null)
//        {
//            // Set the direction based on player orientation
//            if (isFacingRight)
//            {
//                projectileScript.direction = Vector2.right;  // Shoot right if facing right
//            }
//            else
//            {
//                projectileScript.direction = Vector2.left;   // Shoot left if facing left
//            }
//        }

//        // Reduce ammo count after firing
//        currentAmmo--;
//       // Debug.Log("Ammo left: " + currentAmmo);
//        UpdateAmmoUI(); // Update the ammo UI after firing
//    }

//    void ReloadAmmo()
//    {
//        // Check if there's enough reserve ammo to refill max ammo
//        if (reserveAmmo > 0)
//        {
//            // Calculate how much ammo is needed to reload max ammo
//            int ammoNeeded = maxAmmo - currentAmmo;

//            // If there's enough reserve ammo to completely refill max ammo
//            if (reserveAmmo >= ammoNeeded)
//            {
//                reserveAmmo -= ammoNeeded;  // Decrease reserve ammo
//                currentAmmo = maxAmmo;      // Refill max ammo
//            }
//            else
//            {
//                currentAmmo += reserveAmmo; // Refill as much as possible with reserve ammo
//                reserveAmmo = 0;            // Deplete reserve ammo
//            }
//           // Debug.Log("Ammo reloaded. Current ammo: " + currentAmmo + ", Reserve ammo: " + reserveAmmo);
//            UpdateAmmoUI(); // Update the ammo UI after reloading
//        }
//        else
//        {
//          //  Debug.Log("No reserve ammo to reload!");
//        }
//    }

//    // Update the ammo UI (text fields) based on the current ammo and reserve ammo
//    void UpdateAmmoUI()
//    {
//        if (currentAmmoText != null)
//        {
//            currentAmmoText.text = "Ammo: " + currentAmmo.ToString(); // Update current ammo display
//        }

//        if (reserveAmmoText != null)
//        {
//            reserveAmmoText.text = "Reserve: " + reserveAmmo.ToString(); // Update reserve ammo display
//        }
//    }
//}

//ammo of max 10 logic but infinite
//public class PlayerAttack : MonoBehaviour
//{
//    public GameObject projectilePrefab;    // Reference to the projectile prefab
//    public Transform firePoint;            // Point from where the projectile will be fired
//    public bool isFacingRight = true;      // Track player's facing direction (true for right, false for left)

//    public int maxAmmo = 10;              // Maximum ammo the player can have
//    private int currentAmmo;              // Current ammo count

//    void Start()
//    {
//        // Initialize the current ammo to the max ammo at the start
//        currentAmmo = maxAmmo;
//    }

//    void Update()
//    {
//        // Check for attack input (e.g., spacebar or mouse click)
//        if (Input.GetButtonDown("Fire1") && currentAmmo > 0) // Fire only if ammo is available
//        {
//            FireProjectile();
//        }

//        // Example of switching the facing direction (You can implement this based on your input or player movement)
//        if (Input.GetKeyDown(KeyCode.A)) // Example: Press A to face left
//        {
//            isFacingRight = false;
//        }
//        else if (Input.GetKeyDown(KeyCode.D)) // Example: Press D to face right
//        {
//            isFacingRight = true;
//        }

//        // Example of reloading (can be adjusted to your game logic)
//        if (Input.GetKeyDown(KeyCode.R)) // Press R to reload ammo
//        {
//            ReloadAmmo();
//        }
//    }

//    void FireProjectile()
//    {
//        // Instantiate the projectile at the fire point position
//        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

//        // Set the direction of the projectile
//        PlayerProjectile projectileScript = projectile.GetComponent<PlayerProjectile>();
//        if (projectileScript != null)
//        {
//            // Set the direction based on player orientation
//            if (isFacingRight)
//            {
//                projectileScript.direction = Vector2.right;  // Shoot right if facing right
//            }
//            else
//            {
//                projectileScript.direction = Vector2.left;   // Shoot left if facing left
//            }
//        }

//        // Reduce ammo count after firing
//        currentAmmo--;
//        Debug.Log("Ammo left: " + currentAmmo);
//    }

//    void ReloadAmmo()
//    {
//        // Reload the ammo (for example, set it to max ammo)
//        currentAmmo = maxAmmo;
//        Debug.Log("Ammo reloaded!");
//    }
//}

//original script
//public class PlayerAttack : MonoBehaviour
//{
//    public GameObject projectilePrefab;    // Reference to the projectile prefab
//    public Transform firePoint;            // Point from where the projectile will be fired

//    void Update()
//    {
//        // Check for attack input (e.g., spacebar or mouse click)
//        if (Input.GetButtonDown("Fire1")) // "Fire1" is the default input for the left mouse button or Ctrl
//        {
//            FireProjectile();
//        }
//    }

//    void FireProjectile()
//    {
//        // Instantiate the projectile at the fire point position
//        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

//        // Set the direction of the projectile
//        PlayerProjectile projectileScript = projectile.GetComponent<PlayerProjectile>();
//        if (projectileScript != null)
//        {
//            // Set the direction based on player orientation (example: right by default)
//            projectileScript.direction = Vector2.right; // Or use the player's facing direction if applicable

//        }
//    }
//}

