using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.Timeline;

public class RotateShoot : MonoBehaviour
{
    [SerializeField] Transform mainShip;
    [SerializeField] Transform beam, flamer;
    [SerializeField] Transform firePos, sgFirePos1, sgFirePos2, sgFirePos3;
    [SerializeField] GameObject shellPrefab;
    [SerializeField] float rotationSpeed;
    [SerializeField] float cooldown;
    [SerializeField] float beamActiveTime;
    [SerializeField] float shellSpeed;

    float fireCooldown;
    float beamTime;
    bool rotatingRight = false;
    bool rotatingLeft = false;
    [SerializeField] bool gunEquipped = true;
    [SerializeField] bool shotgunEquipped = false;
    [SerializeField] bool beamEquipped = false;
    [SerializeField] bool flameTEquipped = false;

    [SerializeField] AudioSource flameSound, beamSound, gunSound, sgSound;

    bool beamFiring;
    bool beamResetting;
    float beamReset;
    GameObject newShell, newShell1, newShell2, newShell3;

    private void Start()
    {
        beam.gameObject.SetActive(false);
        flamer.gameObject.SetActive(false);
        beamTime = beamActiveTime;
        beamReset = 0f;
        beamResetting = false;
        beamFiring = false;
    }

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

        if (Input.GetKey(KeyCode.Space) && gunEquipped && fireCooldown <= 0)
        {
            FireShell();
        }

        if (Input.GetKey(KeyCode.Space) && shotgunEquipped && fireCooldown <= 0)
        {
            FireShotgun();
        }

        if (Input.GetKeyDown(KeyCode.Space) && beamEquipped)
        {
            if (!beamFiring && beamTime > 0)
            {
                beamFiring = true;
                beamSound.Play();
            }
            ActivateBeam();
        }

        if (Input.GetKey(KeyCode.Space) && flameTEquipped)
        {
            ActivateFlamethrower();
        }
    }

    void FireShell()
    {
        newShell = Instantiate(shellPrefab, firePos.position, Quaternion.identity);
        newShell.GetComponent<Rigidbody2D>().AddForce(firePos.up * shellSpeed, ForceMode2D.Impulse);
        fireCooldown = cooldown;
        gunSound.Play();
    }

    void FireShotgun()
    {
        newShell1 = Instantiate(shellPrefab, sgFirePos1.position, Quaternion.identity);
        newShell2 = Instantiate(shellPrefab, sgFirePos2.position, Quaternion.identity);
        newShell3 = Instantiate(shellPrefab, sgFirePos3.position, Quaternion.identity);
        newShell1.GetComponent<Rigidbody2D>().AddForce(sgFirePos1.up * shellSpeed, ForceMode2D.Impulse);
        newShell2.GetComponent<Rigidbody2D>().AddForce(sgFirePos2.up * shellSpeed, ForceMode2D.Impulse);
        newShell3.GetComponent<Rigidbody2D>().AddForce(sgFirePos3.up * shellSpeed, ForceMode2D.Impulse);
        fireCooldown = cooldown;
        sgSound.Play();
    }

    void ActivateBeam()
    {
        if (beamTime > 0 && beamFiring)
        {
            beam.gameObject.SetActive(true);
        }
    }

    void ActivateFlamethrower()
    {

    }

    // Method for controlling all weapon timers
    void FireTimer()
    {
        // Timer for base gun and shotgun
        if (fireCooldown > 0)
        {
            fireCooldown -= Time.deltaTime;
        }

        // Set of instructions for resetting the beam
        if (beamTime > 0 && beamFiring)
        {
            beamTime -= Time.deltaTime;
            Debug.Log("firing");
        }

        if (beamTime <= 0 && !beamResetting)
        {
            beamFiring = false;
            beam.gameObject.SetActive(false);
            beamReset = 5f;
            beamResetting = true;
            Debug.Log("cooling down");
        }

        if (beamReset > 0)
        {
            beamReset -= Time.deltaTime;
            Debug.Log("charging");
        }

        if (beamReset <= 0 && beamResetting)
        {
            beamTime = beamActiveTime;
            beamResetting = false;
            Debug.Log("beam ready");
        }
    }

    public void EquipBeam()
    {
        beamEquipped = true;
        gunEquipped = false;
        shotgunEquipped = false;
        flameTEquipped = false;
    }

    public void EquipShotgun()
    {
        beamEquipped = false;
        gunEquipped = false;
        shotgunEquipped = true;
        flameTEquipped = false;
    }

    public void EquipGun()
    {
        beamEquipped = false;
        gunEquipped = true;
        shotgunEquipped = false;
        flameTEquipped = false;
    }

    public void EquipFlamer()
    {
        beamEquipped = false;
        gunEquipped = false;
        shotgunEquipped = false;
        flameTEquipped = true;
    }
}
