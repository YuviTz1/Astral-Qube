using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosionManager : MonoBehaviour
{
    Rigidbody rb;
    Vector3 force;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        force.x = Random.Range(-350.0f, 350.0f);    
        force.y = Random.Range(-350.0f, 350.0f);        
        force.z = Random.Range(-350.0f, 350.0f);
        rb.AddForce(force);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
