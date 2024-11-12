using UnityEngine;
using TMPro;  // Ensure TextMeshPro is used

public abstract class PickupItem : MonoBehaviour
{
    public string itemName;            // Name of the item
    public Sprite itemIcon;            // Icon for UI display (if needed)
    public TextMeshProUGUI itemText;   // Reference to TextMeshPro component (UI element)

    public abstract void ApplyEffect(PlayerItem playerItem);

    // This method can be called to display the item's name in the UI (or log it)
    protected void DisplayItemName()
    {
        if (itemText != null)
        {
            itemText.text = "Picked up: " + itemName;  // Display the item name in the UI
        }
        else
        {
            Debug.Log("Picked up: " + itemName);  // Fallback to logging in console if no UI element is assigned
        }
    }

    protected void OnPickup()
    {
        // Display item name when picked up
        DisplayItemName();

        // Destroy the pickup item after use
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ApplyEffect(other.GetComponent<PlayerItem>());
            OnPickup();
        }
    }
}


