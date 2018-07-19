using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControl : MonoBehaviour {

    public float moveSpeed=6f;
    public float rotationSpeed = 80f;
    public float jumpSpeed = 8f;
    public float gravity = 20f;
    Vector3 moveDirection = Vector3.zero;
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        transform.Rotate(0, h * rotationSpeed * Time.deltaTime, 0);
        CharacterController controller = GetComponent<CharacterController>();
        
       

        if (controller.isGrounded)
        {

           
            float v = Input.GetAxis("Vertical");
            moveDirection = new Vector3(0, 0, v);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= moveSpeed;
            walkingAnimation( v);

            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpSpeed;            
                anim.SetBool("IsJumping",true);                                    
            }
            else
            {
                anim.SetBool("IsJumping", false);
            }
         
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);


    }

    void walkingAnimation( float v)
    {
        bool Walking =  v != 0f;
        anim.SetBool("IsWalking", Walking);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            anim.SetBool("IsBattle",true);
        }
        if (gameObject.GetComponent<PlayerHealth>().currentHealth<=0)
        {
            anim.SetBool("IsBattle", false);
            anim.SetBool("IsDead", true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            anim.SetBool("IsBattle", false);
        }
    }
}
