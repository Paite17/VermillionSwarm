using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyType
{
    SUICIDE_BOMBER,
    RANGED
}


public class Enemy : MonoBehaviour
{
    private AIDestinationSetter destination;

    // Start is called before the first frame update
    void Start()
    {
        destination.target = GameObject.Find("MainShip").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
