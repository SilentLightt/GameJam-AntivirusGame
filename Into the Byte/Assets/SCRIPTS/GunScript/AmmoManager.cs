using UnityEngine;
using TMPro;

public class AmmoManager : MonoBehaviour
{
    public int maxAmmo = 10;              // Maximum ammo the player can have
    public int currentAmmo;               // Current ammo count
    public int reserveAmmo = 100;         // Total reserve ammo available

    public TextMeshProUGUI currentAmmoText;  // Reference to display current ammo
    public TextMeshProUGUI reserveAmmoText;  // Reference to display reserve ammo

    void Start()
    {
        // Initialize the current ammo to the max ammo at the start
        currentAmmo = maxAmmo;
        UpdateAmmoUI(); // Update UI at the start
    }

    public bool TryFireAmmo()
    {
        if (currentAmmo > 0)
        {
            currentAmmo--; // Decrease ammo count
            UpdateAmmoUI(); // Update the UI
            return true;
        }
        return false;
    }

    public void ReloadAmmo()
    {
        if (reserveAmmo > 0)
        {
            int ammoNeeded = maxAmmo - currentAmmo;
            if (reserveAmmo >= ammoNeeded)
            {
                reserveAmmo -= ammoNeeded;
                currentAmmo = maxAmmo;
            }
            else
            {
                currentAmmo += reserveAmmo;
                reserveAmmo = 0;
            }
            UpdateAmmoUI();
        }
    }

    public void UpdateAmmoUI()
    {
        if (currentAmmoText != null)
        {
            currentAmmoText.text = "Ammo: " + currentAmmo.ToString();
        }
        if (reserveAmmoText != null)
        {
            reserveAmmoText.text = "Reserve: " + reserveAmmo.ToString();
        }
    }
}

