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
  
    public override void Attack()
    {
        //StartCoroutine(AttackWithDelay());

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

            // Trigger the appropriate attack animation based on direction
            animator.GetComponent<PlayerAnimator>().TriggerRangeAttack(direction);  // Call the animator's method to trigger the correct animation
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
