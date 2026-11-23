using UnityEngine;
using TMPro;

namespace UI
{
    public class GameHUDView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _waveText;
        [SerializeField] private TMP_Text _enemiesText;
        [SerializeField] private TMP_Text _moneyText;
        [SerializeField] private TMP_Text _castleHPText;
        [SerializeField] private GameObject _nextWavePanel;
        [SerializeField] private TMP_Text _nextWaveText;

        public void ShowWave(string text)
        {
            _waveText.text = text;
        }

        public void ShowEnemies(string text)
        {
            _enemiesText.text = text;
        }

        public void ShowMoney(string text)
        {
            _moneyText.text = text;
        }

        public void ShowCastleHP(string text)
        {
            _castleHPText.text = text;
        }

        public void ShowNextWaveTimer(bool visible, string text)
        {
            _nextWavePanel.SetActive(visible);
            if (visible) _nextWaveText.text = text;
        }
    }
}