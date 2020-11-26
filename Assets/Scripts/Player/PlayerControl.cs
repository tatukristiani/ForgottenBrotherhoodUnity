using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    public float playerSpeed = 10f;
    private float horizontalMove = 0f;
    private float verticalMove = 0f;
    private bool playerFacingRight = true;

    private Vector3 direction;
    private Rigidbody2D rigidBody;
    public Animator animator;
   

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
     
    }

    // Update is called once per frame
    void Update()
    {

        GetInput();
        
    }
    
    private void FixedUpdate()
    {
        
        rigidBody.MovePosition(transform.position + direction * playerSpeed * Time.deltaTime);
        
    }

    private void GetInput()
    {

        direction = Vector2.zero;
        horizontalMove = Input.GetAxisRaw("Horizontal") * playerSpeed;
        verticalMove = Input.GetAxisRaw("Vertical") * playerSpeed;

        if(horizontalMove != 0)
        {
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
            
         
        }
        else if(horizontalMove == 0 && verticalMove != 0)
        {
            animator.SetFloat("Speed", Mathf.Abs(verticalMove));
            
        }
        else if(horizontalMove == 0 && verticalMove == 0)
        {
            animator.SetFloat("Speed", 0);
        }

        
        



        if(Input.GetKey(KeyCode.W))
        {
            direction += Vector3.up;

        }
        if(Input.GetKey(KeyCode.S))
        {
            direction += Vector3.down;

        }
        if(Input.GetKey(KeyCode.A))
        {
            if(playerFacingRight)
            {
                Flip();
            }

            direction += Vector3.left;

        }
        if(Input.GetKey(KeyCode.D))
        {
            if(!playerFacingRight)
            {
                Flip();
            }

            direction += Vector3.right;

        }
    }

    //switches the characthers sprite to left/right.
    /*COPIED FROM: https://github.com/Brackeys/2D-Character-Controller/blob/master/CharacterController2D.cs */
    private void Flip()
    {
        // Switch the way the player is labelled as facing
        playerFacingRight = !playerFacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
       
        
    }
}

