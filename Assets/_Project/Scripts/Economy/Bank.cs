using System;
using Models;
using UnityEngine;

namespace Economy
{
    public class Bank : MonoBehaviour
    {
        private const string MONEY_SAVE_KEY = "Money";

        public event Action OnMoneyChanged;

        public int Money => _model.Money;

        private BankModel _model;

        private void Awake()
        {
            int savedMoney = PlayerPrefs.GetInt(MONEY_SAVE_KEY, 0);

            _model = new BankModel(savedMoney);
            _model.OnMoneyChanged += HandleMoneyChanged;
        }

        private void OnDestroy()
        {
            _model.OnMoneyChanged -= HandleMoneyChanged;
        }

        public bool CanAfford(int amount)
        {
            return _model.CanAfford(amount);
        }

        public void AddMoney(int amount)
        {
            _model.AddMoney(amount);
        }

        public void SpendMoney(int amount)
        {
            _model.SpendMoney(amount);
        }


        private void HandleMoneyChanged()
        {
            PlayerPrefs.SetInt("Money", _model.Money);
            PlayerPrefs.Save();

            OnMoneyChanged?.Invoke();
        }
    }
}
