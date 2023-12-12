using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] float points;
    [SerializeField] float wave;
    [SerializeField] GameObject energyField;

    float kills;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Instantiate(energyField);
        }
    }
}
