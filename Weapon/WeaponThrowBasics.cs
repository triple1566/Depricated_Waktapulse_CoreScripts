using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponThrowBasics : MonoBehaviour
{

    [Header("references")]
    [SerializeField] private Transform cam;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private GameObject throwable;

    [Header("Settings")]
    [SerializeField] private int totalThrows;
    [SerializeField] private float throwCoolDown;
    [SerializeField] private float DespawnTime;

    [Header("Keybind")]
    public KeyCode throwKey = KeyCode.Mouse0;
    [SerializeField] private float throwForce;
    [SerializeField] private float throwUpForce;
    private bool readyToThrow;


    // Start is called before the first frame update
    private void Start()
    {
        readyToThrow = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetKey(throwKey) && readyToThrow && totalThrows > 0){
            Throw();
        }
    }

    //================================================

    private void Throw(){
        readyToThrow=false;
        GameObject projectile = Instantiate(throwable, attackPoint.position, cam.rotation);
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();


        Vector3 forceDirection = cam.transform.forward;

        RaycastHit hit;

        if(Physics.Raycast(cam.position,cam.forward, out hit, 500f)){
            forceDirection = (hit.point-attackPoint.position).normalized;
        }

        Vector3 forceToAdd = forceDirection*throwForce+transform.up*throwUpForce;
        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);
        totalThrows--;
        Invoke(nameof(ResetThrow),throwCoolDown);
        Destroy(projectile,DespawnTime);
    }

    private void ResetThrow(){
        readyToThrow = true;
    }
}
