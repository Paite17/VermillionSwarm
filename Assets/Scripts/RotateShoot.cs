using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RotateShoot : MonoBehaviour
{
    [SerializeField] Transform mainShip;
    [SerializeField] Transform firePos;
    [SerializeField] GameObject shellPrefab;
    [SerializeField] float rotationSpeed;
    [SerializeField] float fireCooldown = 0.8f;
    [SerializeField] float shellSpeed;

    bool rotatingRight = false;
    bool rotatingLeft = false;
    GameObject newShell;

    private void Update()
    {
        FireTimer();

        if (Input.GetKey(KeyCode.A) && !rotatingLeft)
        {
            rotatingRight = true;
            if (rotatingRight)
            {
                mainShip.transform.Rotate(new Vector3(0, 0, rotationSpeed) * Time.deltaTime);

            }
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            rotatingRight = false;
        }

        if (Input.GetKey(KeyCode.D) && !rotatingRight)
        {
            rotatingLeft = true;
            if (rotatingLeft)
            {
                mainShip.transform.Rotate(new Vector3(0, 0, -rotationSpeed) * Time.deltaTime);

            }
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            rotatingLeft = false;
        }

        if (Input.GetKey(KeyCode.Space) && fireCooldown <= 0)
        {
            FireShell();
        }
    }

    void FireShell()
    {
        newShell = Instantiate(shellPrefab, firePos.position, Quaternion.identity);
        newShell.GetComponent<Rigidbody2D>().AddForce(firePos.up * shellSpeed, ForceMode2D.Force);
        fireCooldown = 0.8f;
    }

    void FireTimer()
    {
        if (fireCooldown > 0)
        {
            fireCooldown -= Time.deltaTime;
        }
    }
}
