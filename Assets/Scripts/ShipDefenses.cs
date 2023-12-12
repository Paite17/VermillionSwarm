using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDefenses : MonoBehaviour
{
    [SerializeField] Transform focusPos;
    [SerializeField] float rotationSpeed;
    [SerializeField] float hitCapacity;

    private GameObject plr;

    private void Start()
    {
        plr = GameObject.Find("MainShip");
        focusPos = plr.transform;
    }

    private void Update()
    {
        if (transform != null)
        {
            RotateEnergyField();
        }
    }

    void RotateEnergyField()
    {
        transform.RotateAround(focusPos.position, Vector3.forward, 20 * Time.deltaTime);
    }
}
