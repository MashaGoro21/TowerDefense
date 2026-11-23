using Enemies;
using UnityEngine;

namespace Economy
{
    public class EnemyRewardManager : MonoBehaviour
    {
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private Bank _bank;
        [SerializeField] private int _coinReward = 20;

        private void OnEnable()
        {
            _enemySpawner.OnEnemySpawned += HandleEnemySpawned;
        }

        private void OnDisable()
        {
            _enemySpawner.OnEnemySpawned -= HandleEnemySpawned;
        }

        private void HandleEnemySpawned(Enemy enemy)
        {
            enemy.OnDied += HandleEnemyDied;
        }

        private void HandleEnemyDied(Enemy enemy)
        {
            enemy.OnDied -= HandleEnemyDied;
            _bank.AddMoney(_coinReward);
        }
    }
}