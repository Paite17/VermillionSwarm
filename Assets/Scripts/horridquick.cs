using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class horridquick : MonoBehaviour
{
    public Player player;

    private void Start()
    {
        player = GameObject.Find("MainShip").GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Time.timeScale = 0;
            player.deathScreen.gameObject.SetActive(true);
            player.deathSound.Play();
            player.mainSFX.Stop();
            Debug.Log("Player died");
        }
    }
}
