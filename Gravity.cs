using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    private CharacterController controller;
    public float downForce = 1.96f;
    private Vector3 playerVelocity;
    public LayerMask ground;
    bool grounded;
    Vector3 moveDirection;
    public float playerHeight;
    Rigidbody rb;


    // Start is called before the first frame update
    private void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        controller = gameObject.AddComponent<CharacterController>();
    }

    // Update is called once per frame
    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight*0.5f+0.2f, ground);
    
        if(grounded){
            playerVelocity.y = 0f;
        }
        else
        {
            playerVelocity.y+=downForce/1000;
        }
        rb.AddForce(-transform.up*playerVelocity.y, ForceMode.Impulse);
        //중력에 따라 아래로 움직이기

    }
}
