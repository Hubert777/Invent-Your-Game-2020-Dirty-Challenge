using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField]
    public float runspeed;

    public ParticleSystem steps;
    public CharacterController2D controller;

    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    
    void Update() {
        if (Input.GetAxis("Horizontal") != 0)
        {
            steps.Play();
        }

        horizontalMove = Input.GetAxis("Horizontal") * runspeed;
        
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
}
