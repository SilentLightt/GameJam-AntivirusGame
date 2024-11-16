using UnityEngine;

public class PlayerAttackManager : MonoBehaviour
{
    private PlayerMeleeAttack meleeAttack;
    private PlayerRangedAttack rangedAttack;

    private void Start()
    {
        // Cache references to the attack components
        meleeAttack = GetComponent<PlayerMeleeAttack>();
        rangedAttack = GetComponent<PlayerRangedAttack>();
    }

    private void Update()
    {
        HandleAttackInput();
    }

    private void HandleAttackInput()
    {
        // Fire1 for ranged attack
        if (Input.GetButtonDown("Fire1") && rangedAttack != null)
        {
            rangedAttack.Attack();
        }

        // Fire2 for melee attack
        if (Input.GetButtonDown("Fire2") && meleeAttack != null)
        {
            meleeAttack.Attack();
        }
    }
}
