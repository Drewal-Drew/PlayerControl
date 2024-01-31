using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;


public class Control : MonoBehaviour
{

    //Character's body
    Rigidbody rb;

    bool onGround = false;

    Vector2 input;

    //Change of Character's dynamics
    [SerializeField]
    private float speed = 1.0f;

    [SerializeField]
    private float height = 1.0f;

    [SerializeField]
    private float dashSpeed = 1.0f;

    [SerializeField]
    private float sprintS = 0.5f;

    //cooldown abilities

    private float dashCount = 1f;

    private float jumpCount = 2f;

    private float Timer = 0;


    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    
    
    void OnMove(InputValue value) //gets (x,y) input value
    {

        if (onGround)
            OnSprint();
        input = value.Get<Vector2>();
        Debug.Log(input);

        rb.velocity = speed * (input.x * transform.right + input.y * transform.forward) ;

       

    }

    
    void OnJump()
    {
        Debug.Log("JumpCount: " + jumpCount);
        
        if (jumpCount > 0)
        {
            Debug.Log("jumping");
            jumpCount--;
            rb.velocity += height * Vector3.up;
        }
       

    }

    private void OnSprint()
    {
        
        if(onGround)
        {
            Debug.Log("Sprinting");
            rb.velocity += sprintS * (input.x * transform.right + input.y * transform.forward);
        }
        if (!onGround)
            return;

    }
    private void OnDash()
    {

        if(!onGround && dashCount > 0)
        {
           Debug.Log("Dash");
            dashCount--;
            Debug.Log("DashCount: " + dashCount);
           rb.velocity = dashSpeed * (input.x *transform.right + input.y * transform.forward) ;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("On_Ground");

        if (other.gameObject.CompareTag("Ground"))
        {
            onGround = true;
            dashCount = 1;
            jumpCount = 2;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("OFF_Ground");
        if(other.gameObject.CompareTag("Ground"))
        {
            onGround = false;
        }
    }




}
