using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update

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


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    private void Update()
    {
        MyInput();
        DragManager();
        if(onGround){
            Debug.Log("Grounded");
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput(){
        horInput = Input.GetAxisRaw("Horizontal");
        vertInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer(){
        moveDirection = orientation.forward*vertInput+orientation.right*horInput;

        rb.AddForce(moveDirection.normalized*moveSpeed*10f,ForceMode.Force);
    }
    private void DragManager(){
        onGround = Physics.Raycast(transform.position, Vector3.down, playerHeight+0.05f, ground);

    }
}
