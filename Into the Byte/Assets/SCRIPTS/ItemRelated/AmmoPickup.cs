using UnityEngine;
public class AmmoPickup : PickupItem
{
    public int ammoAmount;

    public override void ApplyEffect(PlayerItem playerItem)
    {
        if (playerItem != null)
        {
            playerItem.AddAmmo(ammoAmount);
            Debug.Log($"Picked up {ammoAmount} ammo!"); // Optional: log in the console
        }
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

