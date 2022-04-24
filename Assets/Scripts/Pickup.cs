using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    
    public class Pickup : MonoBehaviour
    {
        public PickUpType type;
        private PlayerData playerData;

        private void Start()
        {
            playerData = PlayerData.instance;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.CompareTag("Player"))
            {
                switch(type)
                {
                    case PickUpType.Health:
                        HealthPickup();
                        break;
                    case PickUpType.Armour:
                        ArmourPickup();
                        break;
                }
            }
        }

        void HealthPickup()
        {
            Destroy(gameObject);
            playerData.Heal(3);
        }
        void ArmourPickup()
        {
            Destroy (gameObject);
            playerData.ArmourUp(1);
        }
    }
}
