using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    [SerializeField] private float downForce = 1.96f;
    private Vector3 playerVelocity;
    [SerializeField] private LayerMask ground;
    bool grounded;
    Vector3 moveDirection;
    [SerializeField] private float playerHeight;
    Rigidbody rb;


    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        //Debug.Log(playerVelocity.y);
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight+0.2f, ground);
        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down)*playerHeight, Color.green);
    
        if(grounded){
            //Debug.Log("grounded by gravity");
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
