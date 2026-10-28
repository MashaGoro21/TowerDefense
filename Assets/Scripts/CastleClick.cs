using UnityEngine;
using UnityEngine.InputSystem;

public class CastleClick : MonoBehaviour
{
    [SerializeField] private CastleUpgradeUI upgradeUI;

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Mouse.current == null) return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            CheckCastleClick(Mouse.current.position.ReadValue());
        }
    }

    private void CheckCastleClick(Vector2 screenPosition)
    {
        Ray ray = mainCamera.ScreenPointToRay(screenPosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform == transform || hit.transform.IsChildOf(transform))
            {
                if (upgradeUI != null) upgradeUI.OpenPanel();
            }
        }
    }
}

