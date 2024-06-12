using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyType
{
    SUICIDE_BOMBER,
    RANGED,
    STEALTH,
    MULTIPLIER,
    MULTIPLIER_SPLIT,
    PI_GUY
}


public class Enemy : MonoBehaviour
{
    // general parameters
    [Header("General Parameters")]
    public EnemyType enemyType;
    [SerializeField] private float enemyDamage = 1f;
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

    // multiplier enemy parameters
    [Header("Multiplier-Specific Parameters")]
    [SerializeField] private int numberOfEnemySpawns;
    [SerializeField] private GameObject splitEnemy; // the gameobject that spawns after death
    

    private Rigidbody2D rb;

    public float EnemyHealth
    {
        get { return enemyHealth; }
        set { enemyHealth = value; }
    }

    public float EnemyDamage
    {
        get { return enemyDamage; }
    }


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerLocation = FindObjectOfType<PlayerStats>().gameObject.transform;

        if (enemyType == EnemyType.PI_GUY)
        {
            int bossBeat = FindObjectOfType<GameManager>().TimesBossWasBeat;

            if (bossBeat > 1)
            {
                IncreasePiGuyHealth(bossBeat);
            }
            FindObjectOfType<UIScript>().maxBossHealth = enemyHealth;
            FindObjectOfType<UIScript>().bossRef = this;
        }
        //StartMoving();

        // remove collisions for enemies as they spawn in 
        // this will probably not work very well, as older enemies will probably still collide with new ones
        // come up with a better solution later
        /*foreach (var e in FindObjectsOfType<Enemy>())
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), e.GetComponent<Collider2D>());
        } */
        
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
                    //transform.rotation = Quaternion.LookRotation(Vector2.right, playerLocation.position);
                }
                

            }

        }

        if (enemyType == EnemyType.PI_GUY)
        {
            transform.position += pathway * movementSpeed * Time.deltaTime;
            rb.velocity = Vector3.zero;
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

        if (enemyHealth <= 0)
        {
            EnemyDeath();
        }

        //transform.LookAt(playerLocation);
    }

    private void ShootProjectile()
    {
        if (!retreating)
        {
            GameObject temp = Instantiate(shootProjectile, firePos.position, Quaternion.identity);

            temp.GetComponent<Rigidbody2D>().AddForce(firePos.right * projectileSpeed, ForceMode2D.Impulse);
        }
    }

    // increases the health of pi guy with each rematch you do
    private void IncreasePiGuyHealth(int bossBeat)
    {
        enemyHealth += (enemyHealth * bossBeat) / 0.5f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                switch (enemyType)
                {
                    case EnemyType.SUICIDE_BOMBER:
                        collision.gameObject.GetComponent<Player>().TakeDamage(enemyDamage);
                        Destroy(gameObject);
                        break;
                    case EnemyType.PI_GUY:
                        collision.gameObject.GetComponent<ShipDefenses>().TakeShieldDamage(enemyDamage);
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
                        collision.gameObject.GetComponent<ShipDefenses>().TakeShieldDamage(enemyDamage);
                        Destroy(gameObject);
                        break;
                    case EnemyType.PI_GUY:
                        collision.gameObject.GetComponent<ShipDefenses>().TakeShieldDamage(enemyDamage);
                        break;
                }
                /*knockback = true;
                collision.gameObject.GetComponent<ShipDefenses>().TakeShieldDamage(enemyDamage);
                Vector2 direction2 = (transform.position - collision.transform.position).normalized;
                //Vector2 force = direction * knockbackForce;
                GetComponent<Rigidbody2D>().AddForce(direction2 * knockbackForce, ForceMode2D.Impulse);*/
                break;
            case "PlayerProj":
                TakeDamage(GameObject.Find("MainShip").GetComponent<PlayerStats>().baseDamage);
                break;
            case "Enemy":
                // ignore collision with other enemies
                Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
                break;


        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "PlayerProj":
                TakeDamage(GameObject.Find("MainShip").GetComponent<PlayerStats>().baseDamage);
                break;
        }
    }

    public void TakeDamage(float amount)
    {
        enemyHealth -= amount;
    }

    public void EnemyDeath()
    {
        if (enemyType == EnemyType.PI_GUY)
        {
            FindObjectOfType<GameManager>().ResetBossCount();
            FindObjectOfType<GameManager>().TimesBossWasBeat++;
            FindObjectOfType<GameManager>().EndOfWave();
            FindObjectOfType<UIScript>().HideBossUI();
        }
        FindObjectOfType<PlayerStats>().AddMoney(20);
        Destroy(gameObject);

        // multiply into the specified amounts
        if (enemyType == EnemyType.MULTIPLIER)
        {

        }
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
