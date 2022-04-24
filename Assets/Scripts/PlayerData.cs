using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class PlayerData
{
    public delegate void OnHealthChange(int maxHealth, int health);
    public delegate void OnScoreChange(int score);
    public delegate void OnPointsTresholdReached(int points);
    public delegate void OnArmourChange(int armour);
    public delegate void OnPlayerDeath();

    public static event OnHealthChange onHealthChange;
    public static event OnScoreChange onScoreChange;
    public static event OnPointsTresholdReached onPointsTresholdReached;
    public static event OnArmourChange onArmourChange;

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
        this.health = 25;
        this.maxHealth = 25;
        this.healthCap = 200;
        this.armour = 0;
        this.maxArmour = 15;
        this.score = 0;
    }

    public void TakeDamage(int damage)
    {
        if(this.armour > 0)
        {
            var tmp = this.armour;
            this.armour -= damage < armour ? damage : armour;
            damage -= tmp;
            onArmourChange?.Invoke(this.armour);
            if (damage <= 0) return;
        }
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

    public void Heal(int heal)
    {
        if (this.health == this.maxHealth) return;
        this.health += this.health + heal > this.maxHealth ? this.maxHealth-this.health : heal;
        onHealthChange?.Invoke(this.maxHealth, this.health);
    }

    public void ArmourUp(int armour)
    {
        if (this.armour == this.maxArmour) return;
        Debug.Log("armour added");
        this.armour += this.armour + armour > this.maxArmour ? this.maxArmour - this.armour : armour;
        onArmourChange?.Invoke(this.armour);
    }

    public void AddScore()
    {
        this.score += 1;
        onScoreChange?.Invoke(this.score);
        if(this.score == 10 || this.score % 20 == 0)
        { 
            onPointsTresholdReached?.Invoke(this.score);
            if(this.score == 20 || this.score == 60 || this.score % 100 == 0)
                LevelUp();
        }
    }
    void LevelUp()
    {
        this.maxHealth += 5;
        this.maxArmour += 3;
    }
}
