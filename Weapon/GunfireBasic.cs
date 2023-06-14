using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: make the gun aim where crosshair is aiming at
//      make the gun fire

public class GunfireBasic : MonoBehaviour
{

    [SerializeField] private LayerMask enemy;
    [SerializeField] private float MaxHitDistance;
    public KeyCode WeaponFireKey = KeyCode.Mouse0;
    public Transform rayOrientation;
    private bool hit;
    private bool readyToFire;



    void Start()
    {
        readyToFire = false;
    }


    void Update()
    {
        hit = Physics.Raycast(transform.position, rayOrientation.TransformDirection(Vector3.forward), MaxHitDistance, enemy);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward)*MaxHitDistance, Color.red);

        if(Input.GetKey(WeaponFireKey)){
            if(hit){
                //aimed and fired
                Debug.Log("hit!");
            }
            else{
                //not aimed but fired
                Debug.Log("Missed");
            }
        }
        else{
            if(hit){
                //aimed but not fired
                Debug.Log("Aimed");
            }
            else{
                //not aimed and not fired
                Debug.Log("Not Aimed");
            }
        }
    }
}
