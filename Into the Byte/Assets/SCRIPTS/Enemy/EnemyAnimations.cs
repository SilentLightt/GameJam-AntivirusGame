using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    public Animator animator;  // Reference to the Animator component

    // Animation triggers
    private readonly string idleTrigger = "Idle";
    private readonly string attackTrigger = "Attack";
    private readonly string walkTrigger = "Walk";
    private readonly string takeDamageTrigger = "TakeDamage";
    private readonly string deathTrigger = "Death";

    void Start()
    {
        // Get the Animator component attached to the enemy
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator not found on the enemy. Please attach an Animator component.");
        }
    }

    // Call this function to play the idle animation
    public void PlayIdleAnimation()
    {
        animator.SetTrigger(idleTrigger);
    }

    // Call this function to play the attack animation
    public void PlayAttackAnimation()
    {
        animator.SetTrigger(attackTrigger);
    }

    // Call this function to play the walk animation
    public void PlayWalkAnimation()
    {
        animator.SetTrigger(walkTrigger);
    }

    // Call this function to play the take damage animation
    public void PlayTakeDamageAnimation()
    {
        animator.SetTrigger(takeDamageTrigger);
    }

    // Call this function to play the death animation
    public void PlayDeathAnimation()
    {
        animator.SetTrigger(deathTrigger);
    }
}

