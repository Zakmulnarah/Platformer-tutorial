using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //so you can use it in other scripts
    public static PlayerController instance;

    //moving
    public float moveSpeed;
    public Rigidbody2D theRB;
    public float jumpForce;
    //jumping
    private bool isGrounded;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;
    //doublejumping
    private bool canDoubleJump;
    //animation
    private Animator anim;
    //flipleftsprite
    private SpriteRenderer theSR;
    //knockback how long it'll last, force
    public float knockBackLength, knockBackForce;
    private float knockBackCounter;

    public float bounceForce;

    public bool stopInput;
    public void Awake()
    {
        instance= this;

    }
    void Start()
    {
        anim = GetComponent<Animator>();
        theSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
       if (!PauseMenu.instance.isPaused && !stopInput)
        {

        
        if (knockBackCounter <= 0)
        {

            theRB.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), theRB.velocity.y);
            //checking if player is on the ground
            isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);

            if (isGrounded)
            {
                canDoubleJump = true;
            }

            if (Input.GetButtonDown("Jump"))
            {
                if (isGrounded)
                {
                    theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                    AudioManager.instance.PlaySFX(10);
                }
                else
                {
                    if (canDoubleJump)
                    {
                        theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                        canDoubleJump = false;
                        AudioManager.instance.PlaySFX(10);
                    }
                }


            }
            if (theRB.velocity.x < 0)
            {
                theSR.flipX = true;
            }
            else if (theRB.velocity.x > 0)
            {
                theSR.flipX = false;
            }
        }
        else
        {
            knockBackCounter -= Time.deltaTime;
            //checking which side character is looking and then pushing him in the opposite direction
            if (!theSR.flipX)
            {
                theRB.velocity = new Vector2(-knockBackForce, theRB.velocity.y);
            } else
            {
                theRB.velocity = new Vector2(knockBackForce, theRB.velocity.y);
            }
        }
        }
        //connecting animation values with the ones in the script, Mathf.Abs turns it into absolute value ex. -0.1 becomes 0.1
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("moveSpeed", Mathf.Abs(theRB.velocity.x));
        
    }
    public void Knockback()
    {
        knockBackCounter = knockBackLength;
        theRB.velocity=new Vector2(0f,knockBackForce);
        
        anim.SetTrigger("hurt");
    }
    public void Bounce()
    {
        theRB.velocity = new Vector2(theRB.velocity.x, bounceForce);
        AudioManager.instance.PlaySFX(10);
    }
}
