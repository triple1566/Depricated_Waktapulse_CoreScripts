using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDespawner : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform projectile;
    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(projectile.position,player.position);
        if(distance>50f){
            Destroy(projectile);
        }
    }
}
