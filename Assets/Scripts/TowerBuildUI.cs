using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerBuildUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TowerPlacementManager placementManager;
    [SerializeField] private TowerBuildingConfig buildingConfig;

    [SerializeField] private Image fastTowerPreview;
    [SerializeField] TMP_Text fastTowerText;
    [SerializeField] private Button fastTowerButton;

    [SerializeField] private Image heavyTowerPreview;
    [SerializeField] private TMP_Text heavyTowerText;
    [SerializeField] private Button heavyTowerButton;

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

    public void BuildFastTower()
    {
        placementManager.BuildTower(0);
        Refresh();
    }

    public void BuildHeavyTower()
    {
        placementManager.BuildTower(1);
        Refresh();
    }

    private void Refresh()
    {
        if (buildingConfig == null) return;

        TowerConfig[] towers = buildingConfig.availableTowers;

        UpdateTowerButton(towers[0], fastTowerPreview, fastTowerText, fastTowerButton);
        UpdateTowerButton(towers[1], heavyTowerPreview, heavyTowerText, heavyTowerButton);
    }

    private void UpdateTowerButton(
        TowerConfig config, 
        Image preview, 
        TMP_Text towerText, 
        Button button)
    {
        bool canAfford = Bank.Instance.Money >= config.price;

        if (preview != null) preview.sprite = config.preview;
        
        if(towerText != null)
            towerText.text = $"{config.towerName} - {config.price} price";

        if (button != null) button.interactable = canAfford;
    }

}
