using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class EnemyRegistry : MonoBehaviour
    {
        public IReadOnlyList<Enemy> Enemies => _enemies;
        
        private List<Enemy> _enemies = new List<Enemy>();

        public void RegisterEnemy(Enemy enemy)
        {
            if (enemy != null && !_enemies.Contains(enemy))
            {
                _enemies.Add(enemy);
            }
        }

        public void UnregisterEnemy(Enemy enemy)
        {
            _enemies.Remove(enemy);
        }
    }
}