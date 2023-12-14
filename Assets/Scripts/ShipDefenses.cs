using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShieldState
{
    ACTIVE,
    RECOVERING
}

public class ShipDefenses : MonoBehaviour
{
    [SerializeField] float hitCapacity;
    [SerializeField] float rechargeTimer;
    [SerializeField] private ShieldState shieldState;

    float rTimer;

    private void Start()
    {

    }

    private void Update()
    {
        ShieldAppearance();


        if (hitCapacity <= 0)
        {
            shieldState = ShieldState.RECOVERING;
            ShieldRecoveryCounter();
        }
    }

    private void ShieldAppearance()
    {
        switch (shieldState)
        {
            case ShieldState.ACTIVE:
                SpriteRenderer current = GetComponent<SpriteRenderer>();
                current.color = new Color32(255, 255, 255, 255);
                GetComponent<PolygonCollider2D>().enabled = true;
                break;
            case ShieldState.RECOVERING:
                SpriteRenderer current2 = GetComponent<SpriteRenderer>();
                current2.color = new Color32(255, 255, 255, 0);
                GetComponent<PolygonCollider2D>().enabled = false;
                break;
        }
    }

    private void ShieldRecoveryCounter()
    {
        rTimer += Time.deltaTime;

        if (rTimer > rechargeTimer)
        {
            hitCapacity = 100;
            shieldState = ShieldState.ACTIVE;
        }
    }

    public void TakeShieldDamage(float damageAmount)
    {
        hitCapacity -= damageAmount;

        if (hitCapacity <= 0)
        {
            //Destroy(gameObject);
            gameObject.SetActive(false);
            FindObjectOfType<GameManager>();
        }
    }
}
