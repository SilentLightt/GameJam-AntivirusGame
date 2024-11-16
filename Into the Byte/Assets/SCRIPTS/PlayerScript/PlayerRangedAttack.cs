using UnityEngine;

public class PlayerRangedAttack : PlayerAttackBase
{
    public GameObject projectilePrefab;    // Reference to the projectile prefab
    public Transform firePoint;            // Point from where the projectile will be fired
    public AmmoManager ammoManager;        // Reference to the AmmoManager script
    public Animator animator;              // Reference to the Animator
    public PlayerController controller;

    public void Start()
    {
        //animator = GetComponent<Animator>();
        //PlayerController controller = GetComponentInChildren<PlayerController>();
    }
    void Update()
    {
        // Check if the current weapon type is set to Ranged before allowing ranged attack
        if (currentWeaponType == WeaponType.Ranged && controller.CanAttack())
        {
            // Input for ranged attack (Fire2)
            if (Input.GetButtonDown("Fire2"))
            {
                Attack();
            }

            // Input for reloading the weapon
            if (Input.GetKeyDown(KeyCode.R)) // Press R to reload
            {
                ammoManager.ReloadAmmo();
            }

            // Rotate the fire point towards the mouse position
            RotateFirePoint();
        }
    }

    public override void Attack()
    {
        if (ammoManager.TryFireAmmo())
        {
            // Trigger ranged attack animation
            animator.SetTrigger("RangedAttack");

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
