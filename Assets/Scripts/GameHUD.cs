using TMPro;
using UnityEngine;

public class GameHUD : MonoBehaviour
{
    [SerializeField] private TMP_Text waveText;
    [SerializeField] private TMP_Text enemiesText;
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text castleHPText;

    [SerializeField] private GameObject nextWavePanel;
    [SerializeField] private TMP_Text nextWaveText;

    [SerializeField] private WaveManager waveManager;
    [SerializeField] private Castle castle;

    private void Start()
    {
        if (nextWavePanel != null)
            nextWavePanel.SetActive(false);
    }

    private void Update()
    {
        UpdateWave();
        UpdateEnemies();
        UpdateMoney();
        UpdateCastleHP();
        UpdateNextWaveTimer();
    }

    private void UpdateWave()
    {
        if (waveManager == null || waveText == null) return;

        waveText.text = $"Wave {waveManager.CurrentWave}/{waveManager.TotalWaves}";
    }

    private void UpdateEnemies()
    {
        if (waveManager == null || enemiesText == null) return;

        enemiesText.text = $"Enemies {waveManager.EnemiesKilled}/{waveManager.EnemiesInWave}";
    }

    private void UpdateMoney()
    {
        if (moneyText == null) return;

        moneyText.text = Bank.Instance.Money.ToString();
    }

    private void UpdateCastleHP()
    {
        if (castle == null || castleHPText == null) return;

        castleHPText.text = $"Castle {castle.CurrentHealth}/{castle.MaxHealth}";
    }

    private void UpdateNextWaveTimer()
    {
        if (waveManager == null || nextWavePanel == null) return;

        bool showTimer = waveManager.IsBetweenWaves;
        if (nextWavePanel.activeSelf != showTimer)
            nextWavePanel.SetActive(showTimer);

        if (showTimer && nextWaveText != null)
        {
            int sec = Mathf.CeilToInt(waveManager.TimeUntilNextWave);
            nextWaveText.text = $"Next wave in {sec} sec";
        }
    }
}
