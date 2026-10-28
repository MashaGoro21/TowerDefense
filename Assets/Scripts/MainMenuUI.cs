using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private string gameSceneName = "Game";

    private void Start()
    {
        Time.timeScale = 1f;
        UpdateMoney();
    }

    private void UpdateMoney()
    {
        if (Bank.Instance != null)
            moneyText.text = Bank.Instance.Money.ToString();
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(gameSceneName);
    }
}
