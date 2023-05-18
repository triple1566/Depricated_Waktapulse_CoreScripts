
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("KeySettings")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode forwardKey = KeyCode.W;
    public KeyCode backwardKey = KeyCode.S;
    public KeyCode rightKey = KeyCode.D;
    public KeyCode leftKey = KeyCode.A;

    [Header("Movement Variables")]
    public float moveSpeed;

    public float groundDrag;
    public float slideDrag;

    public float playerHeight;
    public LayerMask ground;
    bool grounded;

    public Transform orientation;

    float horInput;
    float vertInput;

    public float jumpforce = 20f;
    public float jumpcooldown;
    public float airmultiplier;
    bool readytojump;

    Vector3 moveDirection;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.useGravity = false;
    }

    private void MyInput()
    {
        horInput = Input.GetAxisRaw("Horizontal");
        vertInput = Input.GetAxisRaw("Vertical");
        if(readytojump&&grounded&&Input.GetKey(jumpKey))
            Jump();
            readytojump = false;
            Invoke(nameof(ResetJump), jumpcooldown);
        
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward*vertInput+orientation.right*horInput;   
        if(grounded){
            rb.AddForce(moveDirection.normalized*moveSpeed*10f, ForceMode.Force);
        }
        else if(!grounded)
            rb.AddForce(moveDirection.normalized*moveSpeed*10f*airmultiplier, ForceMode.Force);
    }

    // Update is called once per frame
    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight*0.5f+0.2f, ground);
        MyInput();


        if (grounded&&!(Input.GetKey(forwardKey)||Input.GetKey(backwardKey)||Input.GetKey(leftKey)||Input.GetKey(rightKey)))
            
            rb.drag = groundDrag;
        else if(grounded)
            rb.drag = slideDrag;
        else 
            rb.drag = 0;
    }
    private void FixedUpdate() 
    {
        MovePlayer();
    }
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z); 
        }
    }

    private void Jump(){

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up*jumpforce, ForceMode.Impulse);

    }

    private void ResetJump(){
        readytojump = true;
    }
}
