using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlasmaGun : CombatController
    {
        public delegate void OnShoot(Sound sound);
        public static event OnShoot onShootSound;
        private void OnEnable()
        {
            PlayerData.onPointsTresholdReached += UpgradeWeapon;
            GameState.onGameOver += ResetWeapon;
        }

        private new async void Update()
        {
            base.Update();
            quarter = DetermineQuarter();
            if(Input.GetButton("Fire1") && _canShoot)
            {
                await Shoot();
            }
        }

        private async Task Shoot()
        {
            onShootSound?.Invoke(Sound.Shoot);
            _canShoot = false;
            var bullet = Instantiate(bulletPrefab, gameObject.transform);
            var velocity = CalculateDirectionalVelocity();
            bullet.GetComponent<Rigidbody2D>().velocity = velocity * SHOOT_SPEED;
            bullet.transform.parent = null;
            await Task.Delay(TimeSpan.FromSeconds(1.0 / SHOOT_RATE));
            _canShoot = true;
        }
        private void UpgradeWeapon(int points)
        {
            SHOOT_RATE += 0.5f;
            gameObject.GetComponent<SpriteRenderer>().color = Color.magenta;
        }

        private void ResetWeapon()
        {
            SHOOT_RATE = 1;
            gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        }
    }
}
