// This script manages Camera movement

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Movement : MonoBehaviour
{
    // Start is called before the first frame update

    public float sens_x = 200;
    public float sens_y = 200;

    [SerializeField] private Transform orientation;

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

        transform.rotation = Quaternion.Euler(xRot, yRot, 0);
        orientation.rotation = Quaternion.Euler(0, yRot, 0);
    }
}
