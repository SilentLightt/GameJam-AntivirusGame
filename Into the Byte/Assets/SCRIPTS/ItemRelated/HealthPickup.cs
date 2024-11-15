using UnityEngine;

public class HealthPickup : AutoPickup
{
    public int healthAmount = 20; // Amount of health to restore

    protected override void PerformPickup()
    {
        // Implement health pickup logic
        Debug.Log("Player picked up health!");

        // Add health to the player (replace this with your actual health management logic)
        HealthBar playerHealth = FindObjectOfType<HealthBar>();
        if (playerHealth != null)
        {
            playerHealth.AddHealth(healthAmount);
        }

        Destroy(gameObject); // Destroy the pickup item
    }
}

//public class HealthPickup : PickupItem
//{
//    public int healthAmount = 20;

//    public override void ApplyEffect(PlayerItem playerItem)
//    {
//        if (playerItem != null)
//        {
//            playerItem.RestoreHealth(healthAmount);
//            Debug.Log($"Picked up {healthAmount} health!"); // Optional: log in the console
//        }
//    }
//}
//public class HealthPickup : PickupItem
//{
//    public int healthAmount = 20;

//    public override void ApplyEffect(PlayerItem playerItem)
//    {
//        if (playerItem != null)
//        {
//            playerItem.RestoreHealth(healthAmount);
//            Debug.Log($"Picked up {healthAmount} health!");
//        }
//    }
//}

