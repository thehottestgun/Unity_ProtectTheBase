using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    private PlayerData _playerData;

    private void Start()
    {
        _playerData = PlayerData.instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            _playerData.AddScore();
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
