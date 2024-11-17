using UnityEngine;

public class AmmoPickup : AutoPickup
{
    public int ammoAmount = 10; // Amount of ammo to give

    protected override void HandlePickup()
    {
        // Implement ammo pickup logic
        Debug.Log("Player picked up ammo!");

        // Add ammo to the player (replace this with your actual ammo management logic)
        AmmoManager ammoManager = FindObjectOfType<AmmoManager>();
        if (ammoManager != null)
        {
            ammoManager.reserveAmmo += ammoAmount;
            ammoManager.UpdateAmmoUI();
        }

        Destroy(gameObject); // Destroy the pickup item
    }
}

//public class AmmoPickup : PickupItem
//{
//    public int ammoAmount = 10; // The amount of ammo this pickup provides

//    // Override the ApplyEffect method for this specific pickup type
//    public override void ApplyEffect(PlayerItem player)
//    {
//        if (player != null)
//        {
//            // Increase the player's ammo by ammoAmount
//            player.AddAmmo(ammoAmount);
//            Debug.Log($"Picked up {ammoAmount} ammo!");
//        }
//    }
//}

