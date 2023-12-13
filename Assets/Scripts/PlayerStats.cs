using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float money;
    public float score;
    public float kills;
    public Range turrets = new(0, 4);
    public Range shields = new(0, 8);
    public float baseDamage;
    public float baseHealth;
    public float baseShieldHealth;
}
