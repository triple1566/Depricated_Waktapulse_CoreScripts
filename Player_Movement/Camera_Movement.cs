// This script manages Camera movement

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Movement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float sens_x = 400;
    [SerializeField] private float sens_y = 400;

    [SerializeField] private Transform orientation;

    Vector3 currentEulerAngles;

    Vector3 currentPlayerEulerAngles;

    private float xRot;
    private float yRot;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frames
    private void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X")*Time.deltaTime * sens_x;
        float mouseY = Input.GetAxisRaw("Mouse Y")*Time.deltaTime * sens_y;

        yRot += mouseX;

        xRot -= mouseY;

        xRot = Mathf.Clamp(xRot, -90f, 90f);

        currentEulerAngles = new Vector3(xRot, 0, 0);
        currentPlayerEulerAngles = new Vector3(0, yRot, 0);


        transform.localEulerAngles = currentEulerAngles;
        orientation.localEulerAngles = currentPlayerEulerAngles;
    }
}
