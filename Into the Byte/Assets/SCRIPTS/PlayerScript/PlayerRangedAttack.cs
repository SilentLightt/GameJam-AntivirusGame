//using UnityEngine;

//public class PlayerRangedAttack : PlayerAttackBase
//{
//    public GameObject projectilePrefab;    // Reference to the projectile prefab
//    public Transform firePoint;            // Point from where the projectile will be fired
//    public AmmoManager ammoManager;        // Reference to the AmmoManager script
//    public Animator animator;              // Reference to the Animator
//    public PlayerController controller;

//    public void Start()
//    {
//        Animator animator = GetComponentInParent<Animator>();
//        PlayerController controller = GetComponentInParent<PlayerController>();
//    }
//    void Update()
//    {
//        // Check if the current weapon type is set to Ranged before allowing ranged attack
//        {
//            // Input for ranged attack (Fire2)
//            if (Input.GetButtonDown("Fire2"))
//            {
//                Attack();
//                animator.SetTrigger("RangeAttack");
//            }

//            // Input for reloading the weapon
//            if (Input.GetKeyDown(KeyCode.R)) // Press R to reload
//            {
//                ammoManager.ReloadAmmo();
//            }

//            // Rotate the fire point towards the mouse position
//            RotateFirePoint();
//        }
//    }

//    public override void Attack()
//    {
//        if (ammoManager.TryFireAmmo())
//        {

//            // Get the direction from the fire point to the mouse position
//            Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - firePoint.position).normalized;

//            // Instantiate the projectile
//            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

//            // Set the direction of the projectile
//            PlayerProjectile projectileScript = projectile.GetComponent<PlayerProjectile>();
//            if (projectileScript != null)
//            {
//                projectileScript.direction = direction;
//            }
//        }
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
//}
using UnityEngine;
using System.Collections;
public class PlayerRangedAttack : PlayerAttackBase
{
    public GameObject projectilePrefab;    // Reference to the projectile prefab
    public Transform firePoint;            // Point from where the projectile will be fired
    public AmmoManager ammoManager;        // Reference to the AmmoManager script
    public Animator animator;              // Reference to the Animator
    public PlayerController controller;
    //public float attackDelay = 0.2f;      // Delay before firing the projectile (in seconds)
    private float attackCooldown = 0.3f; // Cooldown in seconds
    private float lastAttackTime = 0f;
    private void Start()
    {
        // Fix for missing Animator and Controller references in Start
        animator = GetComponentInParent<Animator>();
        controller = GetComponentInParent<PlayerController>();
    }

    void Update()
    {
        if (Input.GetButton("Fire2"))
        {
            Attack();
        }

        if (Input.GetKeyDown(KeyCode.R)) // Reload ammo on "R" key press
        {
            ammoManager.ReloadAmmo();
        }

        RotateFirePoint();
    }
    //private IEnumerator AttackWithDelay()
    //{
    //    //Optionally, trigger attack animation before the delay
    //    Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - firePoint.position).normalized;
    //    animator.GetComponent<PlayerAnimator>().TriggerRangeAttack(direction);

    //    // Wait for the specified delay
    //    yield return new WaitForSeconds(attackDelay);

    //    // Proceed to instantiate the projectile after the delay
    //    if (ammoManager.TryFireAmmo())
    //    {
    //        // Instantiate the projectile
    //        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

    //        // Set the direction of the projectile
    //        PlayerProjectile projectileScript = projectile.GetComponent<PlayerProjectile>();
    //        if (projectileScript != null)
    //        {
    //            projectileScript.direction = direction;
    //        }
    //    }
    //}
    //    if (ammoManager.TryFireAmmo())
    //    {
    //        Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - firePoint.position).normalized;

    //        // Trigger the attack animation based on the direction
    //        animator.GetComponent<PlayerAnimator>().TriggerRangeAttack(direction);

    //        // Instantiate the projectile immediately
    //        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

    //        // Set the direction of the projectile
    //        PlayerProjectile projectileScript = projectile.GetComponent<PlayerProjectile>();
    //        if (projectileScript != null)
    //        {
    //            projectileScript.direction = direction;
    //        }

    //        // Wait for the specified delay before enabling the projectile movement
    //        yield return new WaitForSeconds(attackDelay);

    //        // Enable the projectile's movement after the delay (if necessary)
    //        if (projectileScript != null)
    //        {
    //            projectileScript.enabled = true;
    //        }
    //    }

    //}
    public override void Attack()
    {
        //StartCoroutine(AttackWithDelay());
        if (Time.time - lastAttackTime >= attackCooldown) // Check if enough time has passed
        {
            lastAttackTime = Time.time;  // Reset the attack timer

            // Fire the projectile as before
            if (ammoManager.TryFireAmmo())
            {
                Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - firePoint.position).normalized;
                GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

                PlayerProjectile projectileScript = projectile.GetComponent<PlayerProjectile>();
                if (projectileScript != null)
                {
                    projectileScript.direction = direction;
                }

                animator.GetComponent<PlayerAnimator>().TriggerRangeAttack(direction);
            }
        }
    }
        //    if (ammoManager.TryFireAmmo())
        //    {
        //        // Get the direction from the fire point to the mouse position
        //        Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - firePoint.position).normalized;

        //        // Instantiate the projectile
        //        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        //        // Set the direction of the projectile
        //        PlayerProjectile projectileScript = projectile.GetComponent<PlayerProjectile>();
        //        if (projectileScript != null)
        //        {
        //            projectileScript.direction = direction;
        //        }

        //        // Trigger the appropriate attack animation based on direction
        //        animator.GetComponent<PlayerAnimator>().TriggerRangeAttack(direction);  // Call the animator's method to trigger the correct animation
        //    }
        //}

        void RotateFirePoint()
        {
            // Get the direction from the fire point to the mouse position
            Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - firePoint.position).normalized;

            // Calculate the angle to rotate
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Apply the rotation to the firePoint
            firePoint.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

        void TriggerAttackAnimation(Vector2 direction)
        {
            // Determine which animation to trigger based on the mouse direction
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                if (direction.x > 0)
                    animator.SetTrigger("RangeAttackRight");
                else
                    animator.SetTrigger("RangeAttackLeft");
            }
            else
            {
                if (direction.y > 0)
                    animator.SetTrigger("RangeAttackUp");
                else
                    animator.SetTrigger("RangeAttackDown");
            }
        }
    }

