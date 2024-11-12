using UnityEngine;
using TMPro;

public class PlayerItem : MonoBehaviour
{
    public int maxAmmo = 10;                // Max ammo capacity per clip
    public int reserveAmmo = 100;           // Reserve ammo available
    public int currentAmmo;                 // Current ammo in the clip
    public TextMeshProUGUI ammoText;        // TextMeshPro for displaying ammo

    private void Start()
    {
        currentAmmo = maxAmmo;  // Start with a full clip
        UpdateAmmoText();
    }

    // Checks if there's ammo left in the current clip
    public bool HasAmmo() => currentAmmo > 0;

    // Checks if reloading is possible (if there’s reserve ammo and clip isn't full)
    public bool CanReload() => reserveAmmo > 0 && currentAmmo < maxAmmo;

    // Decrease the ammo by 1 each time a shot is fired
    public void ConsumeAmmo()
    {
        currentAmmo = Mathf.Max(currentAmmo - 1, 0);
        UpdateAmmoText();
    }

    // Reload the current clip from the reserve ammo
    public void Reload()
    {
        if (CanReload())
        {
            int ammoNeeded = maxAmmo - currentAmmo;          // Calculate the amount needed to refill the clip
            int ammoToReload = Mathf.Min(ammoNeeded, reserveAmmo); // Only reload what’s available

            currentAmmo += ammoToReload;
            reserveAmmo -= ammoToReload;

            UpdateAmmoText();
        }
    }

    // Adds ammo to the reserve (useful for pickups)
    public void AddAmmo(int amount)
    {
        reserveAmmo += amount;
        UpdateAmmoText();
    }

    // Updates the ammo text display with current and reserve ammo
    private void UpdateAmmoText()
    {
        if (ammoText != null)
        {
            ammoText.text = $"Ammo: {currentAmmo} / {reserveAmmo}";
        }
    }
}



