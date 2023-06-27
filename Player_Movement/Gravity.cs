//This script adds custom gravitational force to the object

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    [SerializeField] private float downForce = 1.96f;
    private Vector3 playerVelocity;
    [SerializeField] private LayerMask ground;
    private bool grounded;
    Vector3 moveDirection;
    [SerializeField] private float playerHeight;
    Rigidbody rb;


    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        //Debug.Log(playerVelocity.y);
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight+0.05f, ground);
        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down)*(playerHeight+0.3f), Color.green);
    
        if(grounded){
            //Debug.Log("grounded by gravity");
            playerVelocity.y = 0f;
        }
        else
        {
            playerVelocity.y+=downForce;
        }
        rb.AddForce(-transform.up*playerVelocity.y, ForceMode.Impulse);
        //중력에 따라 아래로 움직이기

    }
}
