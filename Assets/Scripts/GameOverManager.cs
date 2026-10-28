using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance { get; private set; }

    [SerializeField] private GameOverConfig config;

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject gameHUD;
    [SerializeField] private TMP_Text wavesText;
    [SerializeField] private TMP_Text killsText;
    [SerializeField] private TMP_Text rewardText;

    [SerializeField] private string gameSceneName = "Game";
    [SerializeField] private string menuSceneName = "MainMenu";

    public bool IsGameOver { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
    }

    public void ShowGameOver()
    {
        if (IsGameOver) return;

        IsGameOver = true;

        Time.timeScale = 0f;

        int waves = WaveManager.Instance != null ? WaveManager.Instance.WavesCompleted : 0;
        int kills = WaveManager.Instance != null ? WaveManager.Instance.TotalEnemiesKilled : 0;

        int reward = 0;

        if (config != null)
            reward = waves * config.moneyPerWave + kills * config.moneyPerKill;

        if (Bank.Instance != null) Bank.Instance.AddMoney(reward);

        if (wavesText != null)
            wavesText.text = $"Waves survived: {waves}";

        if (killsText != null)
            killsText.text = $"Enemies killed: {kills}";

        if (rewardText != null)
            rewardText.text = $"Meta coins earned: {reward}";

        if (gameHUD != null)
            gameHUD.SetActive(false);

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(gameSceneName);
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(menuSceneName);
    }
}
