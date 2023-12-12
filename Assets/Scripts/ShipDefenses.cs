using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDefenses : MonoBehaviour
{
    [SerializeField] Transform focusPos;
    [SerializeField] float rotationSpeed;
    [SerializeField] float hitCapacity;

    private void Start()
    {
        focusPos = GetComponent<RotateShoot>().transform;
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
        transform.RotateAround(focusPos.position, Vector3.up, 20 * Time.deltaTime);
    }
}
