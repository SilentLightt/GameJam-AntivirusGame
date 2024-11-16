using UnityEngine;

public class PlayerAttackManager : MonoBehaviour
{
    public PlayerMeleeAttack meleeAttack;
   // public PlayerRangedAttack rangedAttack;
    public Animator animator;  // Reference to the Animator
    public GameObject Gun;
    public GameObject Sword;
    private void Start()
    {
        // Cache references to the attack components
      //  meleeAttack = GetComponent<PlayerMeleeAttack>();
       // rangedAttack = GetComponent<PlayerRangedAttack>();

    }

    private void Update()
    {
        HandleAttackInput();
    }

    private void HandleAttackInput()
    {
        // Fire1 for ranged attack
        //if (Input.GetButton("Fire2") && rangedAttack != null)
        //{
        //    rangedAttack.Attack();
        //    animator.SetTrigger("RangeAttack");  // Trigger the RangeAttack animation
        //    Gun.SetActive(true);
        //    Sword.SetActive(false);
        //}

        // Fire2 for melee attack
        if (Input.GetButtonDown("Fire1") && meleeAttack != null)
        {
            meleeAttack.Attack();
            animator.SetTrigger("MeleeAttack");  // Trigger the MeleeAttack animation
            Sword.SetActive(true);
            Gun.SetActive(false);


        }
    }
}
