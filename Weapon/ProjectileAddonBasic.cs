using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAddonBasic : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody rb;

    private bool targetHit;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    //=================================

    private void OnCollisionEnter(Collision collision){
        if(targetHit){
            return;
        }
        else{
            targetHit = true;
        }
        rb.isKinematic = true;
        transform.SetParent(collision.transform);
    }
}
