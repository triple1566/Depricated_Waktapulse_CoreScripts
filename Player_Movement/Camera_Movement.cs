
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Movement : MonoBehaviour
{
    // Start is called before the first frame update

    public float sens_x = 500;
    public float sens_y = 500;

    public Transform orientation;

    float xRot;
    float yRot;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frames
    void Update()
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
