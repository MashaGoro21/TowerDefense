using UnityEngine;

namespace Economy
{
    public class Bank : MonoBehaviour
    {
        public int Money { get; private set; }

        private void Awake()
        {
            Money = PlayerPrefs.GetInt("Money", 0);
        }

        public void AddMoney(int amount)
        {
            if (amount <= 0) return;
            Money += amount;

            SaveMoney();
        }

        public bool SpendMoney(int amount)
        {
            if (amount <= 0 || Money < amount) return false;

            Money -= amount;
            SaveMoney();

            return true;
        }

        private void SaveMoney()
        {
            PlayerPrefs.SetInt("Money", Money);
            PlayerPrefs.Save();
        }
    }
}
