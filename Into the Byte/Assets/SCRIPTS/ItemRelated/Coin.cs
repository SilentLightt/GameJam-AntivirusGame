using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float pickupRange = 2f;    // Range within which the player can auto-pickup the coin
    public float despawnTime = 30f;   // Time in seconds before the coin disappears
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Assuming the player has the "Player" tag
        StartCoroutine(DespawnAfterTime());
    }

    private void Update()
    {
        if (player != null && Vector3.Distance(transform.position, player.position) <= pickupRange)
        {
            Collect();
        }
    }

    private IEnumerator DespawnAfterTime()
    {
        yield return new WaitForSeconds(despawnTime);
        Destroy(gameObject);  // Destroy the coin if not collected in time
    }

    private void Collect()
    {
        GameManager.Instance.AddCoin(1);
        Destroy(gameObject);
    }
}
