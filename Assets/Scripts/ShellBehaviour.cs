using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellBehaviour : MonoBehaviour
{
    [SerializeField] float lifeTime = 5f;
    

    private void Start()
    {

    }

    private void Update()
    {
        Timer();

        
    }

    void Timer()
    {
        lifeTime -= Time.deltaTime;

        if (lifeTime < 0)
        {
            Destroy(gameObject);
        }
    }

}
