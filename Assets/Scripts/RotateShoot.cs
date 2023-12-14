using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Timeline;

public class RotateShoot : MonoBehaviour
{
    [SerializeField] Transform mainShip;
    [SerializeField] Transform beam, flamer;
    [SerializeField] Transform firePos, sgFirePos1, sgFirePos2, sgFirePos3, backFirePos;
    [SerializeField] GameObject shellPrefab;
    [SerializeField] ParticleSystem flamePS;
    [SerializeField] float cooldown;
    [SerializeField] float beamActiveTime;
    [SerializeField] float flamerActiveTime;
    [SerializeField] float shellSpeed;

    public float rotationSpeed;
    public float fireCooldown;
    float beamTime;
    float flameTime;
    bool rotatingRight = false;
    bool rotatingLeft = false;
    [SerializeField] bool gunEquipped = true;
    [SerializeField] bool shotgunEquipped = false;
    [SerializeField] bool beamEquipped = false;
    [SerializeField] bool flameTEquipped = false;
    [SerializeField] bool backshotEquipped = false;

    [SerializeField] AudioSource flameSound, beamSound, gunSound, sgSound, cdIncomplete;

    bool beamFiring;
    bool flameFiring;
    bool beamResetting;
    bool flameResetting;
    float beamReset;
    float flameReset;
    GameObject newShell, newShell1, newShell2, newShell3, newShell4;

    private void Start()
    {
        beam.gameObject.SetActive(false);
        flamer.gameObject.SetActive(false);
        beamTime = beamActiveTime;
        flameTime = flamerActiveTime;
        beamReset = 0f;
        flameReset = 0f;
        beamResetting = false;
        beamFiring = false;
        flameResetting = false;
        flameFiring = false;

        flamePS = GetComponent<ParticleSystem>();
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
            if (backshotEquipped)
            {
                FireBackshots();
            }
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
            }
            ActivateBeam();

            if (!beamFiring && beamResetting)
            {
                cdIncomplete.Play();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && flameTEquipped)
        {
            if (!flameFiring && flameTime > 0)
            {
                flameFiring = true;
            }
            ActivateFlamethrower();

            if (!flameFiring && flameResetting)
            {
                cdIncomplete.Play();
            }
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
            beamSound.Play();
        }
    }

    void ActivateFlamethrower()
    {
        if (flameTime > 0 && flameFiring)
        {
            flamer.gameObject.SetActive(true);
            flameSound.Play();
            flamePS.Play();
        }
    }

    void FireBackshots()
    {
        newShell4 = Instantiate(shellPrefab, backFirePos.position, Quaternion.identity);
        newShell4.GetComponent<Rigidbody2D>().AddForce(backFirePos.up * shellSpeed, ForceMode2D.Impulse);
        fireCooldown = cooldown;
        gunSound.Play();
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
            beamReset = 7f;
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

        if (flameTime > 0 && flameFiring)
        {
            flameTime -= Time.deltaTime;
            Debug.Log("flaming");
        }

        if (flameTime <= 0 && !flameResetting)
        {
            flameFiring = false;
            flamePS.Stop();
            flamer.gameObject.SetActive(false);
            flameReset = 3f;
            flameResetting = true;
            Debug.Log("cooling down");
        }

        if (flameReset > 0)
        {
            flameReset -= Time.deltaTime;
            Debug.Log("refilling");
        }

        if (flameReset <= 0 && flameResetting)
        {
            flameTime = flamerActiveTime;
            flameResetting = false;
            Debug.Log("flamer ready");
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

    public void EquipBackshot()
    {
        backshotEquipped = true;
    }
}
