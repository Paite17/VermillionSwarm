using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDestroy : MonoBehaviour
{

    private void Start()
    {
        
    }
    // this script has one purpose in life
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                Destroy(gameObject);
                break;
            case "Shield":
                collision.gameObject.GetComponent<ShipDefenses>().HitCapacity--;
                Destroy(gameObject);
                break;
        }
        
    }
}
