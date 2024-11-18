using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance { get; private set; }

    private int coinCount = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        // Load the coin count from PlayerPrefs if it exists
        coinCount = PlayerPrefs.GetInt("CoinCount", 0);
    }

    public void AddCoin(int amount)
    {
        coinCount += amount;
        PlayerPrefs.SetInt("CoinCount", coinCount);
        PlayerPrefs.Save();

        // Update the UI coin count if necessary
        GameManager.Instance.AddCoin(coinCount);
    }

    public int GetCoinCount()
    {
        return coinCount;
    }
}
