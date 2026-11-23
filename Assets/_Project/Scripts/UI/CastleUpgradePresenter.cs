using System;
using Castle;
using Economy;

namespace UI
{
    public class CastleUpgradePresenter : IDisposable
    {
        private readonly CastleUpgradeView _view;
        private readonly CastleUpgradeManager _upgradeManager;
        private readonly Bank _bank;

        public CastleUpgradePresenter(CastleUpgradeView view, CastleUpgradeManager upgradeManager, Bank bank)
        {
            _view = view;
            _upgradeManager = upgradeManager;
            _bank = bank;

            _view.OnHealthClicked += BuyHealth;
            _view.OnAttackSpeedClicked += BuyAttackSpeed;
            _view.OnDamageClicked += BuyDamage;
            _view.OnCloseClicked += ClosePanel;

            _bank.OnMoneyChanged += RefreshView;

            _view.ClosePanel();
            RefreshView();
        }

        public void OpenPanel()
        {
            RefreshView();
            _view.OpenPanel();
        }

        public void ClosePanel()
        {
            _view.ClosePanel();
        }

        public void Dispose()
        {
            _view.OnHealthClicked -= BuyHealth;
            _view.OnAttackSpeedClicked -= BuyAttackSpeed;
            _view.OnDamageClicked -= BuyDamage;
            _view.OnCloseClicked -= ClosePanel;

            _bank.OnMoneyChanged -= RefreshView;
        }

        private void BuyHealth()
        {
            _upgradeManager.UpgradeHealth();
            RefreshView();
        }

        private void BuyAttackSpeed()
        {
            _upgradeManager.UpgradeAttackSpeed();
            RefreshView();
        }

        private void BuyDamage()
        {
            _upgradeManager.UpgradeDamage();
            RefreshView();
        }

        private void RefreshView()
        {
            _view.ShowHealth(
                GetUpgradeText(
                    "Health", 
                    _upgradeManager.CanUpgradeHealth, 
                    _upgradeManager.HealthLevel, 
                    _upgradeManager.HealthCost),
                _upgradeManager.CanUpgradeHealth &&
                _bank.CanAfford(_upgradeManager.HealthCost));

            _view.ShowAttackSpeed(
                GetUpgradeText(
                    "Attack Speed",
                    _upgradeManager.CanUpgradeAttackSpeed,
                    _upgradeManager.AttackSpeedLevel,
                    _upgradeManager.AttackSpeedCost),
                _upgradeManager.CanUpgradeAttackSpeed &&
                _bank.CanAfford(_upgradeManager.AttackSpeedCost));

            _view.ShowDamage(
                GetUpgradeText(
                    "Damage",
                    _upgradeManager.CanUpgradeDamage,
                    _upgradeManager.DamageLevel,
                    _upgradeManager.DamageCost),
                _upgradeManager.CanUpgradeDamage &&
                _bank.CanAfford(_upgradeManager.DamageCost));
        }

        private string GetUpgradeText(string upgradeName, bool canUpgrade, int level, int cost)
        {
            if (!canUpgrade)
                return $"{upgradeName} | Level MAX";

            return $"{upgradeName} | Level {level} | Cost {cost}";
        }
    }
}