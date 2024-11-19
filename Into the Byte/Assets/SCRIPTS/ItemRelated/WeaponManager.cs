using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject[] weapons; // Array to store all available weapons
    private int currentWeaponIndex = 0;

void Start()
{
    if (weapons.Length == 0)
    {
        Debug.LogError("No weapons assigned in the WeaponManager!");
        return;
    }

    foreach (GameObject weapon in weapons)
    {
        if (weapon == null)
        {
            Debug.LogError("Weapon array contains null entries!");
            continue;
        }
        weapon.SetActive(false);
    }

    weapons[currentWeaponIndex]?.SetActive(true);
}

    void Update()
    {
        // Switch weapons with the Q and E keys (or other inputs)
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwitchWeapon(-1); // Switch to the previous weapon
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            SwitchWeapon(1); // Switch to the next weapon
        }
    }

void SwitchWeapon(int direction)
{
    // Log current weapon index and direction
    Debug.Log($"Switching weapon. Current: {currentWeaponIndex}, Direction: {direction}");
    
    // Disable the current weapon
    weapons[currentWeaponIndex].SetActive(false);

    // Update the index based on direction (-1 for previous, 1 for next)
    currentWeaponIndex += direction;

    // Ensure the index loops back around
    if (currentWeaponIndex < 0)
    {
        currentWeaponIndex = weapons.Length - 1;
    }
    else if (currentWeaponIndex >= weapons.Length)
    {
        currentWeaponIndex = 0;
    }

    // Log the new weapon index
    Debug.Log($"New weapon index: {currentWeaponIndex}");

    // Enable the new weapon
    weapons[currentWeaponIndex].SetActive(true);
}

}
