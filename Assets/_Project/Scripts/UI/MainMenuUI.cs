using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _moneyText;
        [SerializeField] private string _gameSceneName = "Game";
        [SerializeField] private Economy.Bank _bank;
        [SerializeField] private Button _startGameButton;

        private void Awake()
        {
            _startGameButton.onClick.AddListener(StartGame);
        }

        private void Start()
        {
            Time.timeScale = 1f;
            UpdateMoney();
        }

        private void OnDestroy()
        {
            _startGameButton.onClick.RemoveListener(StartGame);
        }

        public void StartGame()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(_gameSceneName);
        }

        private void UpdateMoney()
        {
            if (_bank != null)
                _moneyText.text = _bank.Money.ToString();
        }
    }
}