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
    public KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Movement Variables")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float mass;

    [SerializeField] private float groundDrag;
    [SerializeField] private float slideDrag;

    [SerializeField] private float playerHeight;
    [SerializeField] private LayerMask ground;
    bool grounded;

    [SerializeField] private Transform orientation;

    float horInput;
    float vertInput;

    [SerializeField] private float jumpforce = 20f;
    [SerializeField] private float jumpcooldown;
    [SerializeField] private float airmultiplier;
    bool readytojump;

    [Header("Crouch Variables")]

    [SerializeField] private float startYscale;
    [SerializeField] private float crouchYscale;
    [SerializeField] private float crouchForce;
    private bool crouched;

    Vector3 moveDirection;

    Rigidbody rb;
    [SerializeField] private Rigidbody bodyRigidbody;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.mass = mass;
        rb.useGravity = false;
        startYscale = transform.localScale.y;
    }


    private void MyInput()
    {
        horInput = Input.GetAxisRaw("Horizontal");
        vertInput = Input.GetAxisRaw("Vertical");
        if(readytojump&&grounded&&(Input.GetKey(jumpKey)||(Input.GetKey(jumpKey)&&Input.GetKey(crouchKey))))
            Jump();
            readytojump = false;
            Invoke(nameof(ResetJump), jumpcooldown);
        
        //crouch functions
        /*
        if(grounded&&Input.GetKey(crouchKey))
        {
            Crouch();
        }
        else if(crouched)
        {
            UndoCrouch();
        }
        */
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
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight+0.1f, ground);
        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down)*playerHeight, Color.green);
        MyInput();

        if (grounded&&!(Input.GetKey(forwardKey)||Input.GetKey(backwardKey)||Input.GetKey(leftKey)||Input.GetKey(rightKey)))
            rb.drag = groundDrag;
        else if(grounded){
            Debug.Log("grounded");
            rb.drag = slideDrag;
        }
        else{
            Debug.Log("Drag is 0");
            rb.drag = 0;
        }
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
    //웅크리기
    /*private void Crouch(){
        bodyRigidbody.constraints = RigidbodyConstraints.FreezePositionZ;
        bodyRigidbody.constraints = RigidbodyConstraints.FreezePositionX;
        bodyRigidbody.freezeRotation = true;
        rb.constraints = RigidbodyConstraints.FreezePositionZ;
        rb.constraints = RigidbodyConstraints.FreezePositionX;
        rb.freezeRotation = true;
        transform.localScale = new Vector3(transform.localScale.x, crouchYscale, transform.localScale.z);
        rb.AddForce(-transform.up*crouchForce, ForceMode.Impulse);
        crouched = true;
    }
    private void UndoCrouch(){
        bodyRigidbody.constraints = RigidbodyConstraints.None;
        rb.constraints = RigidbodyConstraints.None;
        bodyRigidbody.freezeRotation = false;
        rb.freezeRotation = true;
        rb.AddForce(transform.up*crouchForce, ForceMode.Impulse);
        transform.localScale = new Vector3(transform.localScale.x, startYscale, transform.localScale.z);
        crouched = false;
    }*/
}
