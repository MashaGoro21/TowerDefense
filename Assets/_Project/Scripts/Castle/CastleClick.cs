using UnityEngine;

namespace Castle
{
    public class CastleClick : MonoBehaviour
    {
        [SerializeField] private UI.CastleUpgradeUI _upgradeUI;

        public void HandleClick()
        {
            _upgradeUI.OpenPanel();
        }
    }
}

