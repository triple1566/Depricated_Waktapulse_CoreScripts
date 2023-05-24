using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAngle : MonoBehaviour

{
    public Transform orientation;

    float xRot;
    float yRot;
    private float rayHitDistance;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        //transform.rotation = Quaternion.Euler(xRot, yRot, 0);
        //orientation.rotation = Quaternion.Euler(0, yRot, 0);
    }
}
