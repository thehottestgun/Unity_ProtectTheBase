using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        this.health = 3;
        this.maxHealth = 3;
        this.healthCap = 7;
        this.armour = 0;
        this.maxArmour = 2;
    }
}
