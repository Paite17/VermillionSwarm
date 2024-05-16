using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    public TMP_Text moneyText, scoreText, killsText, healthText;
    public RotateShoot player;
    public Player playerH;

    private void Start()
    {
        baseDamage = 30;
        baseHealth = 5;
        baseShieldHealth = 3;
        shields = 1;
    }

    private void Update()
    {
        UpdateText();
    }

    public void DoubleFireRate()
    {
        if (player.cooldown > 0.126)
        {
            player.cooldown -= 0.125f;
        }
    }

    public void AddDamage()
    {
        if (money >= 50)
        {
            baseDamage += 10;
            money -= 50;
            Debug.Log("damage up");
        }
    }

    public void AddHealth()
    {
        if (money >= 50)
        {
            FindObjectOfType<Player>().Health += 1;
            money -= 50;
            Debug.Log("health up");
        }
    }

    public void AddShieldHealth()
    {
        if (money >= 200)
        {
            baseShieldHealth++;
            money -= 200;
            var shield = FindObjectsOfType<ShipDefenses>();

            foreach (var current in shield)
            {
                current.maxHitCapacity += 1;
            }
        }
        
    }

    public void AddTurret()
    {
        if (turrets < 4 && money >= 200)
        {
            turrets += 1;
            money -= 200;
            Debug.Log("turret up");
        }
    }

    public void AddShield()
    {
        if (shields < 8 && money >= 100)
        {
            shields += 1;
            money -= 100;
            Debug.Log("shield up");
        }
    }

    public void AddTurnSpeed()
    {
        if (money >= 100 && player.rotationSpeed < 360)
        {
            player.rotationSpeed += 30;
            money -= 100;
            Debug.Log("turn speed up");
        }
    }

    public void AddMoney(float amount)
    {
        money += amount;
        score += 100;
        kills += 1;
    }

    void UpdateText()
    {
        moneyText.text = "Stardust: " + $"{money:00000}";
        scoreText.text = "Score: " + $"{score:000000}";
        killsText.text = "Kills: " + $"{kills:0000}";
        healthText.text = "Health: " + $"{playerH.health}";
    }
}
