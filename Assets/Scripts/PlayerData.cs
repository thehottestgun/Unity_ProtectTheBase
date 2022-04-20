using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class PlayerData
{
    public delegate void OnHealthChange(int maxHealth,int health);
    public delegate void OnScoreChange(int score);
    
    public static event OnHealthChange onHealthChange;
    public static event OnScoreChange onScoreChange;

    public int health, maxHealth, healthCap;
    public int armour, maxArmour, score;
    
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
        this.score = 0;
    }

    public void TakeDamage(int damage)
    {
        this.health -= damage;
        onHealthChange?.Invoke(this.maxHealth,this.health);
        if (this.health <= 0)
        {
            this.NewPlayerData();
            onScoreChange?.Invoke(this.score);
            onHealthChange?.Invoke(this.maxHealth, this.health);
            GameState.instance.Reset();
        }
        
    }
    public void AddScore()
    {
        this.score += 1;
        onScoreChange?.Invoke(this.score);
    }
}
