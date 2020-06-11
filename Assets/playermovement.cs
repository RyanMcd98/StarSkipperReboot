using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovement : MonoBehaviour
{

    [SerializeField] private CharacterController2D Controller;
    float HorizontalMove = 0f;
    public float runSpeed = 40f;
    bool jump = false;
    bool crouch = false;
    public Animator animator;

    //walljumping variables
    [SerializeField] private LayerMask LevelGeometryLayer;
    [SerializeField] private Transform leftWallCheck;
    [SerializeField] private Transform rightWallCheck;
    private Rigidbody2D rb;

    private bool touchingLeftWall = false;
    private bool touchingRightWall = false;
    private bool wallSlide = false;
    const float wallCheckRadius = .2f;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetButtonDown("Jump"))
        {
            jump = true;

        }
    }
    public void onLanding()
    {
        animator.SetBool("Isjumping", false);
        animator.SetBool("goingdown", false);

    }

    private void FixedUpdate()
    {
        //use a circle cast to check if the player is touching a wall
        touchingLeftWall = false;
        touchingRightWall = false;
        wallSlide = false;

        //left wall check
        Collider2D[] colliders = Physics2D.OverlapCircleAll(leftWallCheck.position, wallCheckRadius, LevelGeometryLayer);
        for (int it = 0; it < colliders.Length; it++)
        {
            if (colliders[it].gameObject != gameObject)
            {
                //the wall checks are reversed when the controller flips
                if (Controller.m_FacingRight)
                {
                    touchingLeftWall = true;
                }
                else
                {
                    touchingRightWall = true;
                }
                //the player is touching a wall on it's left
            }
        }

        //right wall check
        colliders = Physics2D.OverlapCircleAll(rightWallCheck.position, wallCheckRadius, LevelGeometryLayer);
        for (int it = 0; it < colliders.Length; it++)
        {
            if (colliders[it].gameObject != gameObject)
            {
                //the wall checks are reversed when the controller flips
                if (Controller.m_FacingRight)
                {
                    touchingRightWall = true;
                }
                else
                {
                    touchingLeftWall = true;
                }
                //the player is touching a wall on it's right
            }
        }

        //move character depending on the joystick position
        HorizontalMove = Input.GetAxis("Horizontal") * runSpeed * Time.fixedDeltaTime;

        //if the player is in the air and moving into a wall then start sliding down the wall
        if (!Controller.m_Grounded && touchingLeftWall && Input.GetAxis("Horizontal") < 0)
        {
            wallSlide = true;
        }
        else if (!Controller.m_Grounded && touchingRightWall && Input.GetAxis("Horizontal") > 0)
        {
            wallSlide = true;
        }
        else
        {
            wallSlide = false;
        }

        //if we are not sliding on a wall then move normally
        if (!wallSlide)
        {
            Controller.Move(HorizontalMove, false, jump);
        }
        else
        {//otherwise start wall jumping
            if (jump)
            {
                rb.AddForce(new Vector2(-HorizontalMove * 50, 900));
            }
        }

        jump = false;
    }
}
