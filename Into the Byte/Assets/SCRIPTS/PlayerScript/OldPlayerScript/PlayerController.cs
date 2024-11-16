using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerAttackBase;

public class PlayerController : MonoBehaviour
{
    public float horizontal;
    public float speed = 5f;
    public float jumpingPower = 5f;
    public Rigidbody2D rb;
    public bool isFacingRight;
    private bool canAttack;             // A flag to control if attacking is allowed


    //  private Animator anim;
    public bool isGrounded;
    public LayerMask groundMask;
    public PlayerAttackBase playerAttackBase;
    private PlayerAttackBase.WeaponType currentWeaponType;
    public GameObject meleeWeapon; // Reference to the melee weapon GameObject
    public GameObject rangedWeapon; // Reference to the ranged weapon GameObject

    public void Start()
    {
       // anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        if (playerAttackBase == null)
        {
            playerAttackBase = GetComponent<PlayerAttackBase>();
        }

        // Ensure only the currently active weapon is visible at the start
        UpdateWeaponVisibility();
    }

    public void Update()
    {
            CharacterMovement();
            Flip();
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartCoroutine(SwitchWeaponWithDelay(PlayerAttackBase.WeaponType.Melee));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            StartCoroutine(SwitchWeaponWithDelay(PlayerAttackBase.WeaponType.Ranged));
        }
    }
    //public void SwitchWeapon(PlayerAttackBase.WeaponType weaponType)
    //{
    //    if (playerAttackBase != null)
    //    {
    //        playerAttackBase.currentWeaponType = weaponType;
    //        // Update UI or other components if needed
    //    }
    //    else
    //    {
    //        Debug.LogWarning("playerAttackBase reference is missing!");
    //    }
    //}
    IEnumerator SwitchWeaponWithDelay(PlayerAttackBase.WeaponType newWeaponType)
    {
        canAttack = false;

        // Optional: Trigger the weapon switch animation
        // animator.SetTrigger("SwitchWeapon");

        yield return new WaitForSeconds(0.2f);

        // Use the instance to update the weapon type
        playerAttackBase.currentWeaponType = newWeaponType;
        UpdateWeaponVisibility();

        canAttack = true;
    }
    void UpdateWeaponVisibility()
    {
        // Enable/disable weapons based on the current weapon type
        if (playerAttackBase.currentWeaponType == PlayerAttackBase.WeaponType.Melee)
        {
            meleeWeapon.SetActive(true);
            rangedWeapon.SetActive(false);
        }
        else if (playerAttackBase.currentWeaponType == PlayerAttackBase.WeaponType.Ranged)
        {
            meleeWeapon.SetActive(false);
            rangedWeapon.SetActive(true);
        }
    }
    public bool CanAttack()
    {
        return canAttack;
    }
    private void CharacterMovement()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            isGrounded = false;
        }

        if (Input.GetButtonDown("Jump") && rb.velocity.x != 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.8f);
        }

        AnimationState();
    }
    private void AnimationState()
    {
        if (horizontal < 0f)
        {
           // anim.SetBool("Running", true);
        }
        else if (horizontal > 0f)
        {
            //anim.SetBool("Running", true);
        }
        else
        {
            //anim.SetBool("Running", false);
        }
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private void Flip()
    {               //Flip to the Left                   //Flip to the Right
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector2 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
