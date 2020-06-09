using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public class playermovement : MonoBehaviour
{
    public CharacterController2D Controller;
    float HorizontalMove = 0f;
    public float runSpeed = 40f;
    bool jump = false;
    bool crouch = false;
    public Animator animator;
    public float speed = 0;
    public ParticleSystem dust;
    public float walljumptime = 0.2f;
    public float wallslidespeed = 0.3f;
    public float walldistance = 0.5f;
    bool iswallsliding = false;
    RaycastHit2D wallcheck;
    float jumptime;
    bool Jump = false;
    float facingspeed;
    bool facingleft = false;
    bool facingright = true;




    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            dust.Play();
           
        }

        //if (Input.GetButtonDown("Crouch"))
        //{
          //  crouch = true;
        //}
        //else if (Input.GetButtonUp("Crouch"))
       // {
           // crouch = false;
        //}
    }
     public void onLanding ()
    {
        animator.SetBool("Isjumping", false);
        animator.SetBool("goingdown", false);
        
    }

    void FixedUpdate()
    {
        //move character
        HorizontalMove = Input.GetAxis("Horizontal") * runSpeed;
        speed = HorizontalMove;
        speed = Math.Abs(speed);
        animator.SetFloat("speed", speed);

        //what way is character facing
        if (facingspeed > 0)
        {
            facingright = true;
            facingleft = false;
        }
         (facingspeed < 0)
        {
            facingright = false;
            facingleft = true;
            
        }
        
        Controller.Move(HorizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
        Debug.Log("moving");

        //wall jump

        if (facingright == true)
        {
            wallcheck = Physics2D.Raycast(transform.position, new Vector2(walldistance, 0), walldistance, groundlayer);

        } else
        {
            wallcheck = Physics2D.Raycast(transform.position, new Vector2(-walldistance, 0), walldistance, groundlayer);
        }
        if (wallcheck && !m_Grounded)
        {

        }



        
     

    }
}
