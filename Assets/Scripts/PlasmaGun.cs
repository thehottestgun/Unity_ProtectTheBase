﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlasmaGun : CombatController
    {
        private new async void Update()
        {
            base.Update();
            quarter = DetermineQuarter();
            if(Input.GetButtonDown("Fire1") && _canShoot)
            {
                await Shoot();
            }
        }

        private async Task Shoot()
        {
            _canShoot = false;
            var bullet = Instantiate(bulletPrefab, gameObject.transform);
            var velocity = CalculateDirectionalVelocity();
            bullet.GetComponent<Rigidbody2D>().velocity = velocity * SHOOT_SPEED;
            bullet.transform.parent = null;
            await Task.Delay(TimeSpan.FromSeconds(1.0 / SHOOT_RATE));
            _canShoot = true;
        }
    }
}