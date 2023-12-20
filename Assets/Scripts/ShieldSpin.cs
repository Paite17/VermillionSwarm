using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSpin : MonoBehaviour
{
    [SerializeField] float turnSpeed;

    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, turnSpeed) * Time.deltaTime);
    }
}
