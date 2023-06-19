//This script manages player movement (sprint, jump, WASD movement)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("KeySettings")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode SprintKey = KeyCode.LeftShift;
    public KeyCode forwardKey = KeyCode.W;
    public KeyCode backwardKey = KeyCode.S;
    public KeyCode rightKey = KeyCode.D;
    public KeyCode leftKey = KeyCode.A;
    public KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Movement Variables")]
    private float sprintMultiplier;
    [SerializeField] private float sprintVal;
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

    [SerializeField] private float jumpforce;
    [SerializeField] private float jumpcooldown;
    [SerializeField] private float airmultiplier;
    bool readytojump = true;
    [Header("Slope Handling")]
    [SerializeField] private float slopeSpeedVal;
    private float slopeSpeedMultiplier;
    [SerializeField] private float maxSlopeAngle;
    [SerializeField] private RaycastHit slopeHit;

    [Header("Crouch Variables")]

    [SerializeField] private float startYscale;
    [SerializeField] private float crouchYscale;
    [SerializeField] private float crouchForce;
    private bool crouched;

    Vector3 moveDirection;

    Rigidbody rb;
    [SerializeField] private Rigidbody bodyRigidbody;

    private bool OnSlope(){
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit,playerHeight+0.1f, ground)){
            float slopeAngle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return slopeAngle<maxSlopeAngle&&slopeAngle!=0;
        }
        return false;
    }

    private Vector3 GetSlopeDirection(){
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);
    }

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
        if(readytojump&&OnSlope()&&(Input.GetKey(jumpKey)||(Input.GetKey(jumpKey)&&Input.GetKey(crouchKey)))&&!(Input.GetKey(forwardKey)||Input.GetKey(backwardKey)||Input.GetKey(leftKey)||Input.GetKey(rightKey)))
        {
            Jump();
            readytojump = false;
            Invoke(nameof(ResetJump), jumpcooldown);
        }
        else if(readytojump&&grounded&&(Input.GetKey(jumpKey)||(Input.GetKey(jumpKey)&&Input.GetKey(crouchKey)))&&!(Input.GetKey(forwardKey)||Input.GetKey(backwardKey)||Input.GetKey(leftKey)||Input.GetKey(rightKey)))
        {
            Jump();
            Jump();
            readytojump = false;
            Invoke(nameof(ResetJump), jumpcooldown);
        }
        else if(readytojump&&(OnSlope()||grounded)&&(Input.GetKey(jumpKey)||(Input.GetKey(jumpKey)&&Input.GetKey(crouchKey)))){
            Jump();
            readytojump = false;
            Invoke(nameof(ResetJump), jumpcooldown);
        }
        
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
        if(OnSlope()){
            Debug.Log("On Slope");
            //Debug.Log(slopeSpeedMultiplier);
            rb.AddForce(GetSlopeDirection()*sprintMultiplier*moveSpeed*slopeSpeedMultiplier*10f, ForceMode.Force);

            if(rb.velocity.y>0){
                rb.AddForce(-Vector3.up * 50.0f, ForceMode.Force);
                Debug.Log("DownForce Applied");
            }
            
        }  
        if(grounded){
            rb.AddForce(moveDirection.normalized*sprintMultiplier*moveSpeed*10f, ForceMode.Force);
        }
        else if(!grounded)
            rb.AddForce(moveDirection.normalized*sprintMultiplier*moveSpeed*10f*airmultiplier, ForceMode.Force);
    }

    private void FixedUpdate()
    {
        if(OnSlope()){
            slopeSpeedMultiplier = slopeSpeedVal;
            gameObject.GetComponent<Gravity>().enabled = false;
        }
        else{
            slopeSpeedMultiplier = 1.0f;
            gameObject.GetComponent<Gravity>().enabled = true;
        }
        if(Input.GetKey(SprintKey)){
            sprintMultiplier = sprintVal;
        }
        else
            sprintMultiplier = 1.0f;
        Debug.Log(readytojump);
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight+0.1f, ground);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down)*playerHeight, Color.green);
        MyInput();

        if ((OnSlope()||grounded)&&!(Input.GetKey(forwardKey)||Input.GetKey(backwardKey)||Input.GetKey(leftKey)||Input.GetKey(rightKey))){
            Debug.Log("grounded");
            rb.drag = groundDrag;
        }
        else if(OnSlope()||grounded){
            Debug.Log("grounded");
            rb.drag = slideDrag;
        }
        else{
            Debug.Log("Drag is 0");
            rb.drag = 0;
        }
        MovePlayer();
    }
    private void SpeedControl()
    {
        if(OnSlope()){
            if(rb.velocity.magnitude>moveSpeed){
                rb.velocity = rb.velocity.normalized*moveSpeed;
            }
        }
        else{
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            if(flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z); 
            }
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