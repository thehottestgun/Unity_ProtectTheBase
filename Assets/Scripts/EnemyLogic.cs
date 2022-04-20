using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    private PlayerData _playerData;
    public delegate void OnEnemyDeath(Sound sound);
    public static event OnEnemyDeath onEnemyDeath;
    private void Start()
    {
        _playerData = PlayerData.instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            _playerData.AddScore();
            onEnemyDeath?.Invoke(Sound.EnemyDie);
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
        
    }
}
