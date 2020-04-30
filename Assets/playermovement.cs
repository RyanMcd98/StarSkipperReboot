using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovement : MonoBehaviour
{
    public CharacterController2D Controller;
    float HorizontalMove = 0f;
    public float runSpeed = 40f;
    bool jump = false;
    bool crouch = false;
    public Animator animator;
    
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
           
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

    private void FixedUpdate()
    {
        //move character
        Controller.Move(HorizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
        Debug.Log("moving");

    }
}
