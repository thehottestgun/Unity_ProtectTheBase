using System;
using System.Collections;
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
        private GameObject[] spawners, enemies;
        private Vector3 _playerPos;
        private bool _spawnActive = true;
        private Vector3 _toPlayerVelocity;
        private static GameState GameState;

        private void Start()
        {
            GameState = GameState.instance;
            _playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
            GameState.onGameOver += ClearEnemies;
            ClearEnemies();
            spawners = GameObject.FindGameObjectsWithTag("Spawner");

            PlayerData.onPointsTresholdReached += LevelUp;
        }

        void ClearEnemies()
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (var enemy in enemies) Destroy(enemy);
        }

        private async void Update()
        {
            if (_spawnActive && GameState.State == GameStateType.PLAY)
                await Spawn();
        }

        private async Task Spawn()
        {
            _spawnActive = false;
            var spawnerChoice = UnityEngine.Random.Range(0, spawners.Length);
            await Task.Delay(TimeSpan.FromSeconds(1.0 / SPAWN_RATE));
            _toPlayerVelocity = CalculateVelocity(spawners[spawnerChoice].gameObject);
            var enemy = Instantiate(_enemy, spawners[spawnerChoice].gameObject.transform);
            AttackBase(enemy);
            _spawnActive = true;
        }
        private Vector3 CalculateVelocity(GameObject spawner)
        {
            var vel = new Vector3(-spawner.transform.position.x, -spawner.transform.position.y, 0).normalized;
            return vel;
        }
        private void AttackBase(GameObject enemy)
        {
            enemy.GetComponent<Rigidbody2D>().velocity = _toPlayerVelocity*MOVEMENT_SPEED;
        }

        void LevelUp(int points)
        {
            SPAWN_RATE += .2f;
        }
    }
}
