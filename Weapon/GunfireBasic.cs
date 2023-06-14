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



    void Start()
    {
        
    }


    void Update()
    {
        hit = Physics.Raycast(transform.position, rayOrientation.TransformDirection(Vector3.forward), MaxHitDistance, enemy);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward)*MaxHitDistance, Color.red);

        if(hit&&Input.GetKey(WeaponFireKey)){
            Debug.Log("Hit something");
        }
        else if(hit){
            Debug.Log("Aimed At");
        }
    }
}
