using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CastleUpgradeUI : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private Castle.CastleUpgradeManager _upgradeManager;
        [SerializeField] private Economy.Bank _bank;
        [SerializeField] private Button _healthButton;
        [SerializeField] private Button _attackSpeedButton;
        [SerializeField] private Button _damageButton;
        [SerializeField] private Button _closeButtonPanel;
        [SerializeField] private TMP_Text _healthText;
        [SerializeField] private TMP_Text _attackSpeedText;
        [SerializeField] private TMP_Text _damageText;

        private void Awake()
        {
            _healthButton.onClick.AddListener(BuyHealth);
            _attackSpeedButton.onClick.AddListener(BuyAttackSpeed);
            _damageButton.onClick.AddListener(BuyDamage);
            _closeButtonPanel.onClick.AddListener(ClosePanel);
        }

        private void Start()
        {
            ClosePanel();
            Refresh();
        }

        private void OnDestroy()
        {
            _healthButton.onClick.RemoveListener(BuyHealth);
            _attackSpeedButton.onClick.RemoveListener(BuyAttackSpeed);
            _damageButton.onClick.RemoveListener(BuyDamage);
            _closeButtonPanel.onClick.RemoveListener(ClosePanel);
        }

        private void Update()
        {
            if (_panel != null && _panel.activeSelf) Refresh();
        }

        public void OpenPanel()
        {
            _panel.SetActive(true);
            Refresh();
        }

        public void ClosePanel()
        {
            _panel.SetActive(false);
        }

        public void BuyHealth()
        {
            _upgradeManager.UpgradeHealth();
            Refresh();
        }

        public void BuyAttackSpeed()
        {
            _upgradeManager.UpgradeAttackSpeed();
            Refresh();
        }

        public void BuyDamage()
        {
            _upgradeManager.UpgradeDamage();
            Refresh();
        }

        private void Refresh()
        {
            if (_upgradeManager == null) return;

            UpdateButton(_healthButton,
                _healthText,
                _upgradeManager.CanUpgradeHealth,
                _upgradeManager.HealthLevel,
                _upgradeManager.HealthCost, "Health");

            UpdateButton(_attackSpeedButton,
                _attackSpeedText,
                _upgradeManager.CanUpgradeAttackSpeed,
                _upgradeManager.AttackSpeedLevel,
                _upgradeManager.AttackSpeedCost,
                "Attack Speed");

            UpdateButton(_damageButton,
                _damageText,
                _upgradeManager.CanUpgradeDamage,
                _upgradeManager.DamageLevel,
                _upgradeManager.DamageCost,
                "Damage");
        }

        private void UpdateButton(
            Button button,
            TMP_Text buttonText,
            bool canUpgrade,
            int level,
            int cost,
            string upgradeName)
        {
            bool canAfford = _bank.CanAfford(cost);
            button.interactable = canUpgrade && canAfford;

            if (!canUpgrade)
            {
                buttonText.text = $"{upgradeName} | Level MAX";
            }
            else
            {
                buttonText.text = $"{upgradeName} | Level {level} | Cost {cost}";
            }
        }
    }
}