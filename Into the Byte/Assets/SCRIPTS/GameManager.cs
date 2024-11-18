using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Wave UI")]
    [SerializeField] private TextMeshProUGUI startWaveText;
    [SerializeField] private TextMeshProUGUI prepareNextWaveText;
    [SerializeField] private TextMeshProUGUI defeatedEnemiesText;

    private int defeatedEnemiesCount = 0;
    private int currentWave = 0;

    private void Start()
    {
        ResetUI();
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
