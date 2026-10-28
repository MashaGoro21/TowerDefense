using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CastleUpgradeUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    [SerializeField] private CastleUpgradeManager upgradeManager;

    [SerializeField] private Button healthButton;
    [SerializeField] private Button attackSpeedButton;
    [SerializeField] private Button damageButton;

    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text attackSpeedText;
    [SerializeField] private TMP_Text damageText;

    private void Start()
    {
        ClosePanel();
        Refresh();
    }

    private void Update()
    {
        if (panel != null && panel.activeSelf) Refresh();
    }

    public void OpenPanel()
    {
        panel.SetActive(true);
        Refresh();
    }

    public void ClosePanel()
    {
        panel.SetActive(false);
    }

    public void BuyHealth()
    {
        upgradeManager.UpgradeHealth();
        Refresh();
    }

    public void BuyAttackSpeed()
    {
        upgradeManager.UpgradeAttackSpeed();
        Refresh();
    }

    public void BuyDamage()
    {
        upgradeManager.UpgradeDamage();
        Refresh();
    }

    private void Refresh()
    {
        if (upgradeManager == null) return;

        UpdateButton(healthButton, 
            healthText, 
            upgradeManager.CanUpgradeHealth, 
            upgradeManager.HealthLevel, 
            upgradeManager.HealthCost, "Health");

        UpdateButton(attackSpeedButton, 
            attackSpeedText, 
            upgradeManager.CanUpgradeAttackSpeed, 
            upgradeManager.AttackSpeedLevel, 
            upgradeManager.AttackSpeedCost, 
            "Attack Speed");

        UpdateButton(damageButton,
            damageText,
            upgradeManager.CanUpgradeDamage,
            upgradeManager.DamageLevel,
            upgradeManager.DamageCost,
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
        bool canAfford = Bank.Instance.Money >= cost;
        button.interactable = canUpgrade && canAfford;

        if(!canUpgrade)
        {
            buttonText.text = $"{upgradeName} | Level MAX";
        }
        else
        {
            buttonText.text = $"{upgradeName} | Level {level} | Cost {cost}";
        }
    }
}
