using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleOscillator : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rb;
    [SerializeField]private Transform transform;
    float direction = 1;
    [SerializeField]private int oscillateDist;
    void Start()
    {
        rb=GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for(int i=oscillateDist; i>0; i--){
            rb.AddForce(transform.forward.normalized*direction*10,ForceMode.Force);
        }
        Mirror();
    }

    void Mirror(){
        direction = -direction;
    }
}
