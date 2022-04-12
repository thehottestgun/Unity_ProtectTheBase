using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    
    public class EnemySpawnLogic : EnemyWaveController
    {
        public GameObject _enemy;

        private async void Update()
        {
            await Spawn();
        }

        private async Task Spawn()
        {
            await Task.Delay(TimeSpan.FromSeconds(1.0 / SPAWN_RATE));
            Instantiate(_enemy);
            await AttackBase(_enemy);
        }

        private async Task AttackBase(GameObject enemy)
        {
            while(enemy.transform.position != Vector3.zero)
            {
                enemy.transform.Translate(Vector3.zero);
            }
        }
    }
}
