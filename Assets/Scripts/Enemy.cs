using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyType
{
    SUICIDE_BOMBER,
    RANGED,
    STEALTH,
    MULTIPLIER
}


public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private float enemyDamage;
    [SerializeField] private float movementSpeed;

    private Transform playerLocation;


    // Start is called before the first frame update
    void Start()
    {
        playerLocation = FindObjectOfType<Player>().gameObject.transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = playerLocation.position.x - transform.position.x;
        float y = playerLocation.position.y - transform.position.y;
        float z = transform.position.z;

        Vector3 pathway = new Vector3(x, y, z);

        pathway = pathway.normalized;

        if (enemyType == EnemyType.SUICIDE_BOMBER)
        {
            transform.position += pathway * movementSpeed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                collision.gameObject.GetComponent<Player>().TakeDamage(enemyDamage);
                break;
            case "Shield":
                collision.gameObject.GetComponent<ShipDefenses>().TakeShieldDamage(enemyDamage); 
                break;
        }
    }
}
