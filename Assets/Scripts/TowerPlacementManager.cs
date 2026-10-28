using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class TowerPlacementManager : MonoBehaviour
{
    [SerializeField] private TowerBuildingConfig buildingConfig;
    [SerializeField] private Transform castle;
    [SerializeField] private TowerBuildUI buildUI;
    [SerializeField] private LayerMask groundLayer;

    private Camera mainCamera;
    private Vector3 selectedPosition;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Mouse.current == null) return;
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()) return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            TrySelectGroundPosition(Mouse.current.position.ReadValue());
        }
    }

    private void TrySelectGroundPosition(Vector2 screenPosition)
    {
        Ray ray = mainCamera.ScreenPointToRay(screenPosition);

        if (!Physics.Raycast(ray, out RaycastHit hit, 500f, groundLayer)) return;

        Vector3 position = hit.point;

        if (!IsValidPosition(position)) return;

        selectedPosition = position;

        if (buildUI != null) buildUI.OpenPanel();
    }

    private bool IsValidPosition(Vector3 position)
    {
        if (buildingConfig == null || castle == null) return false;
        if (FlatDistance(position, castle.position) < buildingConfig.minDistanceFromCastle)
            return false;

        TowerAttack[] towers = FindObjectsByType<TowerAttack>(FindObjectsSortMode.None);

        foreach (TowerAttack tower in towers)
        {
            if (FlatDistance(position, tower.transform.position) < buildingConfig.minDistanceBetweenTowers)
                return false;
        }

        return true;
    }

    private float FlatDistance(Vector3 a, Vector3 b)
    {
        a.y = 0f;
        b.y = 0f;

        return Vector3.Distance(a, b);
    }

    public void BuildTower(int towerIndex)
    {
        if (buildingConfig == null) return;
        if (towerIndex < 0 || towerIndex >= buildingConfig.availableTowers.Length) return;

        TowerConfig config = buildingConfig.availableTowers[towerIndex];

        if (!IsValidPosition(selectedPosition)) return;
        if (!Bank.Instance.SpendMoney(config.price)) return;

        GameObject tower = Instantiate(config.prefab, selectedPosition, Quaternion.identity);
        TowerAttack towerAttack = tower.GetComponent<TowerAttack>();
        towerAttack.SetConfig(config);

        if (buildUI != null) buildUI.ClosePanel();
    }
}
