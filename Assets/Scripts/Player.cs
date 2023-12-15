using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float health;
    // PUT UPGRADE LIST HERE 

    public float Health
    {
        get { return health; }
        set { health = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0)   
        {
            // GAMEOVER SEQUENCE
            Debug.Log("Player died");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Enemy":
                TakeDamage(collision.gameObject.GetComponent<Enemy>().EnemyDamage);
                break;
            case "Projectile":
                // hard coded :DDDD
                TakeDamage(1f);
                break;
        }
    }

}
