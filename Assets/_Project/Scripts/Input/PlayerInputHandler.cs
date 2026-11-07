using Castle;
using Towers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Input
{
    public class PlayerInputHandler : MonoBehaviour
    {
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private TowerPlacementManager _towerPlacementManager;
        [SerializeField] private CastleClick _castleClick;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private float _maxRaycastDistance = 500f;

        private void Update()
        {
            if (Mouse.current == null) return;

            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
                return;

            if (!Mouse.current.leftButton.wasPressedThisFrame) return;

            Ray ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (!Physics.Raycast(ray, out RaycastHit hit, _maxRaycastDistance))
                return;

            if (hit.collider.TryGetComponent(out CastleClick castleClick))
            {
                castleClick.HandleClick();
                return;
            }

            if ((_groundLayer.value & (1 << hit.collider.gameObject.layer)) != 0)
            {
                _towerPlacementManager.TrySelectGroundPosition(hit.point);
            }
        }
    }
}
