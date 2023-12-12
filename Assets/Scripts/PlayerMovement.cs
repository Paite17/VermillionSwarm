using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    private Vector2 moveDirection;
    private bool canMove;
    private bool isMoving;
    private Animator animator;

    private PlayerMovement player;


    private void Start()
    {
        canMove = true;       
    }

    private void Awake()
    {
       
    }




    //bool introDone = false;
    // for specifically colliding with the prologue object
    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove)
        {
            return;
        }
        ProcessInputs();

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        //animator.SetBool("isMoving", isMoving);

    }

    // for physics calculations and other general stuff that doesn't need to change often
    void FixedUpdate()
    {
        Move();


        // my if statements are longer than yours (detecting inputs to add to step count)
        /*
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            // prevents 6 thousand errors per second on the start screen because i'm bad at programming
            
                // animator shits
                float moveX = Input.GetAxisRaw("Horizontal");
                float moveY = Input.GetAxisRaw("Vertical");
                animator.SetFloat("moveX", moveX);
                animator.SetFloat("moveY", moveY);
            

        }
    } */

       
    }

    // takes the inputs of the player and identifies what sort of direction to take
    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    // move
    // my comments are so insightful
    void Move()
    {

        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);


        // trying desperately to stop the player moving during the battle transition lol
        if (!canMove)
        {
            rb.velocity = new Vector2(0, 0);
        }
    }
}
