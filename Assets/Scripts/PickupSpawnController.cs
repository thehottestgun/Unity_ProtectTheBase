using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class PickupSpawnController : MonoBehaviour
    {
        public GameObject health, armour;
        private void Start()
        {
            EnemyLogic.onItemCanSpawn += SpawnItem;
        }

        private void SpawnItem(Vector3 loc, Vector3 vel)
        {
            var spawnItem = UnityEngine.Random.value;
            if (spawnItem < 0.65f) return;
            var pickup = Instantiate(spawnItem > 0.8f ? health : armour, loc,Quaternion.identity);
            pickup.GetComponent<Rigidbody2D>().velocity = vel;
        }
    }
}
