using UnityEngine;

public class Bank : MonoBehaviour
{
    public static Bank Instance { get; private set; }

    public int Money { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Money = PlayerPrefs.GetInt("Money", 0);

    }

    public void AddMoney(int amount)
    {
        if (amount <= 0) return;
        Money += amount;

        PlayerPrefs.SetInt("Money", Money);
        PlayerPrefs.Save();
    }

    public bool SpendMoney(int amount)
    {
        if (amount <= 0 || Money < amount) return false;

        Money -= amount;

        PlayerPrefs.SetInt("Money", Money);
        PlayerPrefs.Save();

        return true;
    }
}
