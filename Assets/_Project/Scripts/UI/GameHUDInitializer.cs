using UnityEngine;
using Enemies;
using Economy;
using Castle;

namespace UI
{
    public class GameHUDInitializer : MonoBehaviour
    {
        [SerializeField] private GameHUDView _view;
        [SerializeField] private WaveManager _waveManager;
        [SerializeField] private Bank _bank;
        [SerializeField] private CastleHealth _castleHealth;

        private GameHUDPresenter _presenter;

        private void Start()
        {
            _presenter = new GameHUDPresenter(_view, _waveManager, _bank, _castleHealth);
        }

        private void OnDestroy()
        {
            _presenter?.Dispose();
        }
    }
}