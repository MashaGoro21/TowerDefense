using TMPro;
using UnityEngine;

namespace UI
{
    public class GameHUD : MonoBehaviour
    {
        [SerializeField] private TMP_Text _waveText;
        [SerializeField] private TMP_Text _enemiesText;
        [SerializeField] private TMP_Text _moneyText;
        [SerializeField] private TMP_Text _castleHPText;
        [SerializeField] private GameObject _nextWavePanel;
        [SerializeField] private TMP_Text _nextWaveText;
        [SerializeField] private Enemies.WaveManager _waveManager;
        [SerializeField] private Economy.Bank _bank;
        [SerializeField] private Castle.CastleHealth _castleHealth;

        private void Start()
        {
            if (_nextWavePanel != null)
                _nextWavePanel.SetActive(false);
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
            if (_waveManager == null || _waveText == null) return;

            _waveText.text = $"Wave {_waveManager.CurrentWave}/{_waveManager.TotalWaves}";
        }

        private void UpdateEnemies()
        {
            if (_waveManager == null || _enemiesText == null) return;

            _enemiesText.text = $"Enemies {_waveManager.EnemiesKilled}/{_waveManager.EnemiesInWave}";
        }

        private void UpdateMoney()
        {
            if (_moneyText == null) return;

            _moneyText.text = _bank.Money.ToString();
        }

        private void UpdateCastleHP()
        {
            if (_castleHealth == null || _castleHPText == null) return;

            _castleHPText.text = $"Castle {_castleHealth.CurrentHealth}/{_castleHealth.MaxHealth}";
        }

        private void UpdateNextWaveTimer()
        {
            if (_waveManager == null || _nextWavePanel == null) return;

            bool showTimer = _waveManager.IsBetweenWaves;
            if (_nextWavePanel.activeSelf != showTimer)
                _nextWavePanel.SetActive(showTimer);

            if (showTimer && _nextWaveText != null)
            {
                int seconds = Mathf.CeilToInt(_waveManager.TimeUntilNextWave);
                _nextWaveText.text = $"Next wave in {seconds} sec";
            }
        }
    }
}