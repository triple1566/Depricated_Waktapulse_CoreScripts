//This script manages gunfire based on raycast.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunfireBasic : MonoBehaviour
{

    [SerializeField] private LayerMask enemy;

    [SerializeField] private LayerMask obstacle;
    [SerializeField] private float MaxHitDistance;
    [SerializeField] private float fireRateCooltime;
    public KeyCode WeaponFireKey = KeyCode.Mouse0;
    public Transform rayOrientation;
    private bool hit;
    private bool obstacleHit;
    private bool readyToFire;



    private void ResetReadyToFire(){
        readyToFire = true;
    }

    private void Start()
    {
        readyToFire = true;
    }


    private void Update()
    {
        hit = Physics.Raycast(transform.position, rayOrientation.TransformDirection(Vector3.forward), MaxHitDistance, enemy);
        obstacleHit = Physics.Raycast(transform.position, rayOrientation.TransformDirection(Vector3.forward), MaxHitDistance, obstacle);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward)*MaxHitDistance, Color.red);

        if(obstacleHit){
            return;
        }

        if(readyToFire&&Input.GetKey(WeaponFireKey)){
            if(hit){
                //aimed and fired
                Debug.Log("hit!");
            }
            else{
                //not aimed but fired
                Debug.Log("Missed");
            }
            readyToFire=false;
            Invoke(nameof(ResetReadyToFire), fireRateCooltime);
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
