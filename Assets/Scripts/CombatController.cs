using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
   
    public class CombatController : MonoBehaviour
    {
        public float SHOOT_RATE;
        public float SHOOT_SPEED;
        public GameObject bulletPrefab;

        private Rigidbody2D _parentRb;
        protected int quarter;
        protected bool _canShoot = true;

        protected void Start()
        {
            _parentRb = gameObject.transform.parent.gameObject.GetComponent<Rigidbody2D>();
        }
        protected void Update()
        {
            ResetRotation();
        }
        protected int DetermineQuarter()
        {
            float rotation = _parentRb.rotation >= 0 ? _parentRb.rotation : (360 - (_parentRb.rotation * -1));

            if (rotation >= 0 && rotation <= 90) return 1;
            else if (rotation > 90 && rotation <= 180) return 2;
            else if (rotation > 180 && rotation <= 270) return 3;
            else return 4;
        }

        public Vector3 CalculateDirectionalVelocity()
        {
            float modifier = 0f;
            Vector3 res = Vector3.zero;
            float rotation = _parentRb.rotation >= 0 ? _parentRb.rotation : (360 - (_parentRb.rotation * -1));
            Debug.Log(rotation);
            switch(quarter)
            {
                case 1:
                    modifier = 1.0f / 90.0f * rotation;
                    res = new Vector3(1 - modifier, modifier, 0);
                    break;
                case 2:
                    modifier = 1.0f / 90.0f * (rotation) -1;
                    res = new Vector3(-modifier, 1 - modifier, 0);
                    break;
                case 3:
                    modifier = 1.0f / 90.0f * (rotation) - 2;
                    res = new Vector3(-1+modifier, -modifier, 0);
                    Debug.Log(res);
                    break;
                case 4:
                    modifier = 1.0f / 90.0f * (rotation) - 3;
                    res = new Vector3(modifier, -1+modifier, 0);
                    break;
            }
            return res;
        }

        public void ResetRotation()
        {
            if (_parentRb.rotation < 360 && _parentRb.rotation > -360) return;
            _parentRb.rotation = 0;
        }
    }
}
