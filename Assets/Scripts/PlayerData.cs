using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class PlayerData
{
    public int health, maxHealth, healthCap;
    public int armour, maxArmour;
    
    private static PlayerData _instance;
    public static PlayerData instance
    {
        get
        {
            if(PlayerData._instance == null)
                PlayerData._instance = new PlayerData();
            return PlayerData._instance;
        }
        set => PlayerData._instance = value;
    }

    protected PlayerData() => this.NewPlayerData();

    private void NewPlayerData()
    {
        this.health = 50;
        this.maxHealth = 50;
        this.healthCap = 200;
        this.armour = 0;
        this.maxArmour = 25;
    }

    public void TakeDamage(int damage)
    {
        this.health -= damage;
        if (this.health <= 0) SceneManager.LoadScene(0);
        Debug.Log(this.health);
    }
}
