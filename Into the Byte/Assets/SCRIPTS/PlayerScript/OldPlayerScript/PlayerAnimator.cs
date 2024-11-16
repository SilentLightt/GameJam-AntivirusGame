using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    private PlayerController playerController;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        bool isJumping = !playerController.isGrounded;

        animator.SetBool("IsWalking", horizontal != 0);
        animator.SetBool("IsJumping", isJumping);
    }

    public void TriggerAttack()
    {
        animator.SetTrigger("RangeAttack");
        animator.SetTrigger("MeleeAttack");
    }
}

//original script
//using UnityEngine;

//public class PlayerAnimator : MonoBehaviour
//{
//    private Animator animator; // Reference to the Animator component
//    private PlayerController playerController;
//    private PlayerAttack playerAttack;

//    void Start()
//    {
//        // Get references to the required components
//        animator = GetComponent<Animator>();
//        playerController = GetComponent<PlayerController>();
//        playerAttack = GetComponentInChildren<PlayerAttack>();

//        if (animator == null)
//        {
//            Debug.LogError("Animator component not found!");
//        }

//        if (playerController == null)
//        {
//            Debug.LogError("PlayerController component not found!");
//        }

//        if (playerAttack == null)
//        {
//            Debug.LogError("PlayerAttack component not found!");
//        }
//    }

//    void Update()
//    {
//        if (animator == null || playerController == null)
//            return;

//        HandleMovementAnimations();
//        HandleAttackAnimations();
//    }

//    private void HandleMovementAnimations()
//    {
//        // Idle or Walking
//        if (playerController.horizontal != 0)
//        {
//            animator.SetBool("IsWalking", true);
//        }
//        else
//        {
//            animator.SetBool("IsWalking", false);
//        }

//        // Jumping
//        if (!playerController.isGrounded)
//        {
//            animator.SetBool("IsJumping", true);
//        }
//        else
//        {
//            animator.SetBool("IsJumping", false);
//        }
//    }

//    private void HandleAttackAnimations()
//    {
//        if (playerAttack != null && Input.GetButtonDown("Fire1"))
//        {
//            animator.SetTrigger("Attack");
//        }

//        if (playerAttack != null && Input.GetButton("Fire2")) // Assume Fire2 is the aim button
//        {
//            animator.SetBool("IsAiming", true);
//        }
//        else
//        {
//            animator.SetBool("IsAiming", false);
//        }
//    }
//}

