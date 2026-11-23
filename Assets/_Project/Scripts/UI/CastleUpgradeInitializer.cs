using Castle;
using Economy;
using UnityEngine;

namespace UI {
    public class CastleUpgradeInitializer : MonoBehaviour
    {
        [SerializeField] private CastleUpgradeView _view;
        [SerializeField] private CastleUpgradeManager _upgradeManager;
        [SerializeField] private Bank _bank;
        [SerializeField] private CastleClick _castleClick;

        private CastleUpgradePresenter _presenter;

        private void Start()
        {
            _presenter = new CastleUpgradePresenter(_view, _upgradeManager, _bank);
            _castleClick.OnCastleClicked += _presenter.OpenPanel;
        }

        private void OnDestroy()
        {
            _castleClick.OnCastleClicked -= _presenter.OpenPanel;
            _presenter.Dispose();
        }
    }
}