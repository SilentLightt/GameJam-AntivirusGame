using UnityEngine;
using TMPro;
using System.Collections;
public abstract class AutoPickup : MonoBehaviour
{
    public TextMeshProUGUI pickupText; // Reference to the TextMeshPro for display
    public string displayMessage = "Press E to pick up"; // Default message
    public string equippedMessage = "Item has been equipped"; // Message to display when item is equipped

    private bool isPlayerInRange = false;

    void Start()
    {
        if (pickupText != null)
        {
            pickupText.gameObject.SetActive(false); // Hide the text initially
        }
    }

    void Update()
    {
        // Check if the player is in range and presses the pickup key
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(PerformPickup());
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Ensure the collider belongs to the player
        {
            isPlayerInRange = true;
            ShowPickupMessage();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            HidePickupMessage();
        }
    }

    void ShowPickupMessage()
    {
        if (pickupText != null)
        {
            pickupText.text = displayMessage;
            pickupText.gameObject.SetActive(true); // Show the pickup text
        }
    }

    void HidePickupMessage()
    {
        if (pickupText != null)
        {
            pickupText.gameObject.SetActive(false); // Hide the pickup text
        }
    }
    protected IEnumerator PerformPickup()
    {
        // Call the specific item pickup logic
        HandlePickup();

        // Display equipped message
        if (pickupText != null)
        {
            pickupText.text = equippedMessage;
            pickupText.gameObject.SetActive(true);
        }

        // Wait for 2 seconds to display the message
        yield return new WaitForSeconds(2);

        // Hide the message and destroy the item
        if (pickupText != null)
        {
            pickupText.gameObject.SetActive(false);
        }

        Destroy(gameObject);
    }
    // Abstract method for specific pickup behavior
    protected abstract void HandlePickup();
}



