using System;

namespace Models
{
    public class BankModel
    {
        public event Action OnMoneyChanged;

        public int Money { get; private set; }

        public BankModel(int initialMoney)
        {
            if (initialMoney < 0)
                throw new ArgumentOutOfRangeException(nameof(initialMoney));

            Money = initialMoney;
        }

        public bool CanAfford(int amount)
        {
            return amount >= 0 && Money >= amount;
        }

        public void AddMoney(int amount)
        {
            if (amount <= 0) return;
            Money += amount;

            OnMoneyChanged?.Invoke();
        }

        public void SpendMoney(int amount)
        {
            Money -= amount;
            OnMoneyChanged?.Invoke();
        }
    }
}
