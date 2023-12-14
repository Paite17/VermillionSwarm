using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyType
{
    SUICIDE_BOMBER,
    RANGED,
    STEALTH,
    MULTIPLIER,
    PI_GUY
}


public class Enemy : MonoBehaviour
{
    // general parameters
    [Header("General Parameters")]
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private float enemyDamage;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float knockbackForce;
    [SerializeField] private float enemyHealth;


    private float knockbackTimer;
    private bool knockback;
    private Transform playerLocation;
    public bool retreating;
    private float retreatTimer;

    // ranged enemy parameters
    [Header("Ranged-Specific Parameters")]
    [SerializeField] private bool inPlayerRange;
    private float shootTimer;
    [SerializeField] private GameObject shootProjectile;
    [SerializeField] private float maxTimeUntilShoot;
    [SerializeField] private Transform firePos;
    [SerializeField] private float projectileSpeed;

    



    // Start is called before the first frame update
    void Start()
    {
        playerLocation = FindObjectOfType<Player>().gameObject.transform;
        //StartMoving();
    }

    // Update is called once per frame
    void Update()
    {
        float x = playerLocation.position.x - transform.position.x;
        float y = playerLocation.position.y - transform.position.y;
        float z = transform.position.z;

        Vector3 pathway = new Vector3(x, y, 0);

        pathway = pathway.normalized;

        if (enemyType == EnemyType.SUICIDE_BOMBER)
        {
            if (!knockback)
            {
                if (!retreating)
                {
                    transform.position += pathway * movementSpeed * Time.deltaTime;
                }
                

            }

        }

        if (enemyType == EnemyType.RANGED && !inPlayerRange)
        {
            if (!retreating)
            {
                transform.position += pathway * movementSpeed * Time.deltaTime;
            }
            
        }
        else if (enemyType == EnemyType.RANGED && inPlayerRange)
        {
            shootTimer += Time.deltaTime;

            Vector3 diff = (playerLocation.position - transform.position);
            float angle = Mathf.Atan2(diff.y, diff.x);
            transform.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg);
        }

        if (shootTimer >= maxTimeUntilShoot)
        {
            if (enemyType == EnemyType.RANGED)
            {
                ShootProjectile();
                shootTimer = 0;
            }
        }

        if (knockback)
        {
            KnockbackTimer();
        }

        if (retreating)
        {
            transform.position += -pathway * movementSpeed * Time.deltaTime;
            retreatTimer += Time.deltaTime;
        }

        if (retreatTimer >= 5f)
        {
            Destroy(gameObject);
        }

        //transform.LookAt(playerLocation);
    }

    private void ShootProjectile()
    {
        GameObject temp = Instantiate(shootProjectile, firePos.position, Quaternion.identity);

        temp.GetComponent<Rigidbody2D>().AddForce(firePos.right * projectileSpeed, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                switch (enemyType)
                {
                    case EnemyType.SUICIDE_BOMBER:
                        Destroy(gameObject);
                        break;
                }
                /*knockback = true;
                collision.gameObject.GetComponent<Player>().TakeDamage(enemyDamage);
                Vector2 direction = (transform.position - collision.transform.position).normalized;
                //Vector2 force = direction * knockbackForce;
                GetComponent<Rigidbody2D>().AddForce(direction * knockbackForce, ForceMode2D.Impulse); */
                break;
            case "Shield":
                switch (enemyType)
                {
                    case EnemyType.SUICIDE_BOMBER:
                        Destroy(gameObject);
                        break;
                }
                /*knockback = true;
                collision.gameObject.GetComponent<ShipDefenses>().TakeShieldDamage(enemyDamage);
                Vector2 direction2 = (transform.position - collision.transform.position).normalized;
                //Vector2 force = direction * knockbackForce;
                GetComponent<Rigidbody2D>().AddForce(direction2 * knockbackForce, ForceMode2D.Impulse);*/
                break;
        }
    }

    public void EnemyDeath()
    {
        FindObjectOfType<PlayerStats>().AddMoney(20);
        Destroy(gameObject);
    }

    private void KnockbackTimer()
    {
        knockbackTimer += Time.deltaTime;

        if (knockbackTimer > 2.5)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            knockback = false;
            knockbackTimer = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "RangedEnemyAttackRange":
                inPlayerRange = true;
                break;
        }
    }
}
