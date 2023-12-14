using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float money;
    public float score;
    public float kills;
    public float turrets;
    public float shields;
    public float baseDamage;
    public float baseHealth;
    public float baseShieldHealth;

    private void Start()
    {
        baseDamage = 10;
        baseHealth = 5;
        baseShieldHealth = 5;
        shields = 1;
    }

    void AddDamage()
    {
        if (money >= 50)
        {
            baseDamage += 10;
            money -= 50;
        }
    }

    void AddHealth()
    {
        if (money >= 50)
        {
            baseHealth += 1;
            money -= 50;
        }
    }

    void AddShieldHealth()
    {

    }

    void AddTurret()
    {
        if (turrets < 4 && money >= 200)
        {
            turrets += 1;
            money -= 200;
        }
    }

    void AddShield()
    {
        if (shields < 8 && money >= 100)
        {
            shields += 1;
            money -= 100;
        }
    }
}
