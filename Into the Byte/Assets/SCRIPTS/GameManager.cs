using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Wave UI")]
    [SerializeField] private TextMeshProUGUI startWaveText;
    [SerializeField] private TextMeshProUGUI prepareNextWaveText;
    [SerializeField] private TextMeshProUGUI defeatedEnemiesText;
    [Header("UI Elements")]

    [SerializeField] private TextMeshProUGUI coinCountText; // Reference to the UI Text that displays the coin count

    private int defeatedEnemiesCount = 0;
    private int currentWave = 0;
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
    private void Start()
    {
        UpdateCoinCountUI();

        ResetUI();
    }
    public void AddCoin(int amount)
    {
        coinCount += amount;
        PlayerPrefs.SetInt("CoinCount", coinCount);
        PlayerPrefs.Save();

        UpdateCoinCountUI();
    }

    private void UpdateCoinCountUI()
    {
        if (coinCountText != null)
        {
            coinCountText.text = "Coins: " + coinCount.ToString();
        }
    }

    public int GetCoinCount()
    {
        return coinCount;
    }

    public void StartWave(int waveNumber)
    {
        currentWave = waveNumber;
        defeatedEnemiesCount = 0;
        UpdateDefeatedEnemiesText();

        startWaveText.text = "Wave " + waveNumber + " Starting!";
        StartCoroutine(HideTextAfterDelay(startWaveText, 2f));  // Hide after 2 seconds
    }

    public void PrepareNextWave(int waveNumber)
    {
        prepareNextWaveText.text = "Preparing Wave " + waveNumber + "...";
        StartCoroutine(HideTextAfterDelay(prepareNextWaveText, 2f));
    }

    public void EnemyDefeated()
    {
        defeatedEnemiesCount++;
        UpdateDefeatedEnemiesText();
    }

    private void UpdateDefeatedEnemiesText()
    {
        defeatedEnemiesText.text = "Enemies Defeated: " + defeatedEnemiesCount;
    }

    private IEnumerator HideTextAfterDelay(TextMeshProUGUI text, float delay)
    {
        yield return new WaitForSeconds(delay);
        text.text = "";
    }

    private void ResetUI()
    {
        startWaveText.text = "";
        prepareNextWaveText.text = "";
        defeatedEnemiesText.text = "Enemies Defeated: 0";
    }
}
