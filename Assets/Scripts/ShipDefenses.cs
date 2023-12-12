using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDefenses : MonoBehaviour
{
    [SerializeField] Transform focusPos;
    [SerializeField] float rotationSpeed;
    [SerializeField] float hitCapacity;

    bool readyToSpin = true;

    private void Start()
    {

    }

    private void Update()
    {
        if (readyToSpin)
        {
            RotateEnergyField();
        }
    }

    void RotateEnergyField()
    {
        transform.RotateAround(focusPos.position, Vector3.forward, rotationSpeed * Time.deltaTime);

    }
}
