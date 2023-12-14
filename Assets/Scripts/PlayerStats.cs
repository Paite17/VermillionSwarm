using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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

    public TMP_Text moneyText, scoreText, killsText;
    public RotateShoot player;

    private void Start()
    {
        baseDamage = 10;
        baseHealth = 5;
        baseShieldHealth = 5;
        shields = 1;
    }

    private void Update()
    {
        UpdateText();
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
            baseHealth += 1;
            money -= 50;
            Debug.Log("health up");
        }
    }

    public void AddShieldHealth()
    {

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
    }

    void UpdateText()
    {
        moneyText.text = "Stardust: " + $"{money:00000}";
        scoreText.text = "Score: " + $"{score:000000}";
        killsText.text = "Kills: " + $"{kills:0000}";
    }
}
