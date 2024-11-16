using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public abstract class PlayerAttackBase : MonoBehaviour
{
    public enum WeaponType
    {
        Melee,
        Ranged
    }

    public WeaponType currentWeaponType = WeaponType.Melee;  // Default to Melee
    public abstract void Attack();
}
