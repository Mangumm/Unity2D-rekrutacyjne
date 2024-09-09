using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControllerFlipped : MonoBehaviour
{
    /*

    Simple 2D platformer,
    most of the important stuff is put in this file

    */



    //Change skin of character in editor, default 96px/96px
    SpriteRenderer spriteRenderer;

    //Change acceleration of character
    public float speed = 13;

    //Change jumping force
    public float jumpForce = 24;

    public Transform groundCheck;
    public LayerMask groundLayer;

    private Rigidbody2D rb2d;
    private SpriteRenderer playerSprite;
    private bool doubleJump;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D> ();
        playerSprite = GetComponent<SpriteRenderer> ();
    }

    void Update()
    {
        //Character movement left/right inputs instead of "getKey"
        rb2d.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, rb2d.velocity.y);
        
        //Character double jumping
        if (isGrounded() && !Input.GetButton("Jump"))
        {
            doubleJump = false;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded() || doubleJump)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, -jumpForce);

                doubleJump = !doubleJump;
            }
        }

        if (Input.GetButtonUp("Jump") && -rb2d.velocity.y > 0f)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y * 0.5f);
        }

        //Makes the character look left/right depending on movement on X axis
        if(Input.GetAxisRaw("Horizontal") > 0)
            {
                playerSprite.flipX = true;
            }
        else if(Input.GetAxisRaw("Horizontal") < 0)
            {
                playerSprite.flipX = false;
            }
    }
    //Check is character on the ground by hitting floor
    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.5f, groundLayer);
    }
}