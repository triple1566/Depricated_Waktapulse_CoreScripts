using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Keybind")]
    public KeyCode jumpKey = KeyCode.Space;
    
    [Header("Movement")]
    private Rigidbody rb;

    private float horInput;
    private float vertInput;
    private Vector3 moveDirection;
    [SerializeField] private Transform orientation;
    [SerializeField] private float moveSpeed;
    [Header("Grounded")]
    private bool onGround;
    [SerializeField] private float playerHeight;
    [SerializeField] private LayerMask ground;

    [Header("Drag")]
    [SerializeField] private float groundDrag;
    [SerializeField] private float airDrag=5.0f;
    private float drag;
    [Header("Jump")]
    private bool readyToJump;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCoolDown;

    //=======================================================
    private void Start()
    {
        readyToJump = true;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    private void Update()
    {
        MyInput();
        DragManager();
        SpeedControl();

        Debug.Log("Grounded = " + onGround);
        Debug.Log("readytojump = " + readyToJump);
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    //========================================================

    private void MyInput(){
        horInput = Input.GetAxisRaw("Horizontal");
        vertInput = Input.GetAxisRaw("Vertical");
        if(Input.GetKey(jumpKey) && onGround && readyToJump){
            readyToJump = false;
            Jump();
            Invoke(nameof(ReadyJump),jumpCoolDown);
        }
    }

    private void MovePlayer(){
        moveDirection = orientation.forward*vertInput+orientation.right*horInput;

        rb.AddForce(moveDirection.normalized*moveSpeed*10f,ForceMode.Force);
    }
    private void DragManager(){
        onGround = Physics.Raycast(transform.position, Vector3.down, playerHeight+0.1f, ground);
        if(onGround){
            drag = groundDrag;
        }
        else{
            drag = airDrag;
        }
        rb.drag = drag;
    }
    private void SpeedControl(){
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if(flatVel.magnitude>moveSpeed){
            Vector3 limitedVel = flatVel.normalized*moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump(){
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up*jumpForce, ForceMode.Impulse);
    }
    private void ReadyJump(){
        readyToJump = true;
    }
}
